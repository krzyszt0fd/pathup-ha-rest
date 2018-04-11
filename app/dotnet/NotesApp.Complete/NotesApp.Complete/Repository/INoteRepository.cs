using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Skeleton.Model;

namespace NotesApp.Skeleton.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetNotes();

        Task Save(Note note);
    }
}