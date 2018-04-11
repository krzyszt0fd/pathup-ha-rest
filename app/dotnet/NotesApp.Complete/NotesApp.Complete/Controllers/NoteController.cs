using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Complete.Model;
using NotesApp.Complete.Repository;

namespace NotesApp.Complete.Controllers
{
    [Route("api/[controller]")]    
    [Produces("application/json")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public Task<IEnumerable<Note>> Get()
        {
            return _noteRepository.GetNotes();
        }
       
        [HttpPost]
        public Task Post(Note model)
        {
            return _noteRepository.Save(model);
        }
    }
}
