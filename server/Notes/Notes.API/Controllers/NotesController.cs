using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.API.ViewModels;
using Notes.Data.Abstract;
using Notes.Model.Entities;

namespace Notes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NotesController(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDetailViewModel> GetStoryDetail(string id)
        {
            var story = _noteRepository.GetSingle(s => s.Id == id, s => s.Owner);
            return _mapper.Map<NoteDetailViewModel>(story);
        }

        [HttpGet("user/{id}")]
        public ActionResult<NotesViewModel> Get(string id)
        {
            var notes = _noteRepository.FindBy(story => story.OwnerId == id);
            return new NotesViewModel
            {
                Notes = notes.Select(_mapper.Map<NoteViewModel>).ToList()
            };
        }

        [HttpPost]
        public ActionResult<NoteCreationViewModel> Post([FromBody] NoteUpdateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownerId = HttpContext.User.Identity.Name;
            var creationTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            var noteId = Guid.NewGuid().ToString();
            var note = new Note
            {
                Id = noteId,
                Title = model.Title,
                Content = model.Content,
                CreationTime = creationTime,
                LastEditTime = creationTime,
                OwnerId = ownerId,
            };

            _noteRepository.Add(note);
            _noteRepository.Commit();

            return new NoteCreationViewModel
            {
                NoteId = noteId
            };
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(string id, [FromBody] NoteUpdateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownerId = HttpContext.User.Identity.Name;
            if (!_noteRepository.IsOwner(id, ownerId)) return Forbid("You are not the owner of this note");

            var existingNote = _noteRepository.GetSingle(id);
            existingNote.Title = model.Title;
            existingNote.LastEditTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            existingNote.Content = model.Content;

            _noteRepository.Update(existingNote);
            _noteRepository.Commit();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var ownerId = HttpContext.User.Identity.Name;
            if (!_noteRepository.IsOwner(id, ownerId)) return Forbid("You are not the owner of this note.");

            _noteRepository.DeleteWhere(story => story.Id == id);
            _noteRepository.Commit();

            return NoContent();
        }
    }
}
