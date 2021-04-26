using FlusherFinder.Data;
using FlusherFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Services
{
    public class NoteService
    {
        private readonly Guid _creatorId;

        public NoteService(Guid creatorId)
        {
            _creatorId = creatorId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    CreatorId = _creatorId,
                    //RatingId = model.RatingId,
                    NoteTitle = model.NoteTitle,
                    NoteContent = model.NoteContent,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.CreatorId == _creatorId)
                        .Select(
                            e =>
                                new NoteListItem
                                {
                                    NoteId = e.NoteId,
                                    NoteTitle = e.NoteTitle
                                }
                                );

                return query.ToArray();
            }
        }

        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == id && e.CreatorId == _creatorId);
                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        RatingId = entity.RatingId,
                        //LocationId = entity.LocationId,
                        //LocationName = entity.LocationName,
                        NoteTitle = entity.NoteTitle,
                        NoteContent = entity.NoteContent,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == model.NoteId && e.CreatorId == _creatorId);
                entity.NoteTitle = model.NoteTitle;
                entity.NoteContent = model.NoteContent;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.CreatorId == _creatorId);
                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
