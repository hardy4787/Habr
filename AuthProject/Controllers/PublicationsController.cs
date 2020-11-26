using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProject.Entities;
using AuthProject.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly DataContext _store;
        public PublicationsController(DataContext store)
        {
            _store = store;
        }
        public ActionResult<List<Publication>> GetPublications()
        {
            return _store.Publications.ToList();
        }
    }
}
