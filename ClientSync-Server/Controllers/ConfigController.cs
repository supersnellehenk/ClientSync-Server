using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClientSync_Server.Features.Startup;
using ClientSync_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientSync_Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly GenerateFileHashes _fileHashes;

        public ConfigController(GenerateFileHashes fileHashes)
        {
            _fileHashes = fileHashes;
        }

        [HttpGet]
        public List<FileHash> Get()
        {
            return _fileHashes.ConfigList;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var fileHash = _fileHashes.ConfigList.Single(filehash => filehash.Id == id);
            var stream = new FileStream(fileHash.Path, FileMode.Open, FileAccess.Read);
            return File(stream, "application/octet-stream", fileHash.Id.ToString());
        }
    }
}