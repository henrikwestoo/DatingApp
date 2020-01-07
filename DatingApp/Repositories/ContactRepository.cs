using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace DatingApp.Repositories
{
    public class ContactRepository : BaseRepository
    {
        public ContactRepository(AppDbContext ctx) : base(ctx)
        {

        }

        public void AddContact(ContactModel model)
        {
            Ctx.Contacts.Add(model);
        }

        public List<int> FindContacts(int profileId, bool accepted)
        {
            List<int> contacts = new List<int>();

            if (accepted)
            {
                contacts.AddRange(Ctx.Contacts.Where(p => (p.ContactId == profileId) && (p.Accepted == accepted)).
                    Select((x) => x.ProfileId).
                    ToList());

                contacts.AddRange(Ctx.Contacts.Where(p => (p.ProfileId == profileId) && (p.Accepted == accepted)).
                    Select((x) => x.ContactId).
                    ToList());
            }
            else
            {
                contacts.AddRange(Ctx.Contacts.Where((p) => (p.ContactId == profileId) && (p.Accepted == accepted)).
                    Select((x) => x.ProfileId).
                    ToList());
            }

            return contacts;
        }

        public void EditContact(int userProfileId, int contactId)
        {
            var contact = Ctx.Contacts.Where((c) => (c.ContactId == userProfileId) && (c.ProfileId == contactId)).First();
            contact.Accepted = true;

            Ctx.Set<ContactModel>().AddOrUpdate(contact);
        }

        public void RemoveContact(int userProfileId, int contactId)
        {

            var contactTo = Ctx.Contacts.Where((c) => (c.ContactId == userProfileId) && (c.ProfileId == contactId)).FirstOrDefault();

            if (contactTo != null)
            {
                Ctx.Set<ContactModel>().Remove(contactTo);
            }
            else
            {
                var contactFrom = Ctx.Contacts.Where((c) => (c.ContactId == contactId) && (c.ProfileId == userProfileId)).First();
                Ctx.Set<ContactModel>().Remove(contactFrom);
            }
        }
    }
}