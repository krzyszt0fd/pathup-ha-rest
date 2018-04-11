using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using NotesApp.Skeleton.Model;

namespace NotesApp.Skeleton.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IDynamoDBContext context;

        public NoteRepository(IDynamoDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Note>> GetNotes()
        {
            var notes = new List<Note>();
            var search = context.ScanAsync<Note>(Enumerable.Empty<ScanCondition>());
            while(!search.IsDone)
            {
                notes.AddRange(await search.GetNextSetAsync());
            }
            return notes;
        }

        public Task Save(Note note)
        {
            return context.SaveAsync(note);
        }
        
    }
}