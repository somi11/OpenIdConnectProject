﻿using AutoMapper;
using ImageGallery.API.Services;
using ImageGallery.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ImageGallery.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public ImagesController(
            IGalleryRepository galleryRepository,
            IWebHostEnvironment hostingEnvironment,
            IMapper mapper)
        {
            _galleryRepository = galleryRepository ?? 
                throw new ArgumentNullException(nameof(galleryRepository));
            _hostingEnvironment = hostingEnvironment ?? 
                throw new ArgumentNullException(nameof(hostingEnvironment));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            var OwnerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            Console.WriteLine(User.Claims);

            if(OwnerId == null)
            {
                throw new Exception("sub claim does not exist");
            }

            // get from repo

            var imagesFromRepo = await _galleryRepository.GetImagesAsync(OwnerId);

            // map to model
            var imagesToReturn = _mapper.Map<IEnumerable<Image>>(imagesFromRepo);

            var userClaimBuilder = new StringBuilder();

            foreach (var claim in User.Claims)
            {
                userClaimBuilder.AppendLine($"Claim Type : {claim.Type} : Claim Value : {claim.Value}");
            }
            Console.WriteLine(userClaimBuilder.ToString());
            // return
            return Ok(imagesToReturn);
        }

        [HttpGet("{id}", Name = "GetImage")]
        [Authorize ("mustOwnImage")]
        public async Task<ActionResult<Image>> GetImage(Guid id)
        {
           

            var imageFromRepo = await _galleryRepository.GetImageAsync(id);

            if (imageFromRepo == null)
            {
                return NotFound();
            }

            var imageToReturn = _mapper.Map<Image>(imageFromRepo);

            return Ok(imageToReturn);
        }

        // ...

        [HttpPost()]
        //[Authorize (Roles = "PayingUser")]
        [Authorize(Policy = "CanAddImage")]
        [Authorize(Policy = "clientApplicationCanWrite")]
        public async Task<ActionResult<Image>> CreateImage([FromBody] ImageForCreation imageForCreation)
        {
            // Automapper maps only the Title in our configuration
            var imageEntity = _mapper.Map<Entities.Image>(imageForCreation);

            // Create an image from the passed-in bytes (Base64), and 
            // set the filename on the image

            // get this environment's web root path (the path
            // from which static content, like an image, is served)
            var webRootPath = _hostingEnvironment.WebRootPath;

            // create the filename
            string fileName = Guid.NewGuid().ToString() + ".jpg";

            // the full file path
            var filePath = Path.Combine($"{webRootPath}/images/{fileName}");

            // write bytes and auto-close stream
            await System.IO.File.WriteAllBytesAsync(filePath, imageForCreation.Bytes);

            // fill out the filename
            imageEntity.FileName = fileName;

            // ownerId should be set - can't save image in starter solution, will
            // be fixed during the course
            //imageEntity.OwnerId = ...;
            var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (ownerId == null)
            {
                throw new Exception("sub claim does not exist");

            }
            Console.WriteLine(ownerId);
            imageEntity.OwnerId = ownerId;
            // add and save.  
            _galleryRepository.AddImage(imageEntity);

            await _galleryRepository.SaveChangesAsync();

            var imageToReturn = _mapper.Map<Image>(imageEntity);
        
            return CreatedAtRoute("GetImage",
                new { id = imageToReturn.Id },
                imageToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize("mustOwnImage")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {            
            var imageFromRepo = await _galleryRepository.GetImageAsync(id);

            if (imageFromRepo == null)
            {
                return NotFound();
            }

            _galleryRepository.DeleteImage(imageFromRepo);

            await _galleryRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize("mustOwnImage")]
        public async Task<IActionResult> UpdateImage(Guid id, 
            [FromBody] ImageForUpdate imageForUpdate)
        {
            var imageFromRepo = await _galleryRepository.GetImageAsync(id);
            if (imageFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(imageForUpdate, imageFromRepo);

            _galleryRepository.UpdateImage(imageFromRepo);

            await _galleryRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}