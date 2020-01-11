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

        //används för att byta kategori på en kontakt
        public void EditCategory(int currentUserId, int contactId, Category newCategory) 
        {
            //eftersom man klickade på kontakten finns den i sambandstabellen, frågan är vilket id som står i vilken kolumn
            //därför har vi två linq frågor, skillnaderna ligger i where-satserna
            var contact = Ctx.Contacts.Where((c) => (c.ContactId == contactId) && (c.ProfileId == currentUserId)).FirstOrDefault();

            if (contact == null) {

                contact = Ctx.Contacts.Where((c) => (c.ContactId == currentUserId) && (c.ProfileId == contactId)).First();
                contact.ProfileCategory = newCategory;
                Ctx.Set<ContactModel>().AddOrUpdate(contact);

            } else
            {
                contact.ContactCategory = newCategory;
                Ctx.Set<ContactModel>().AddOrUpdate(contact);
            }
            
        

        }

        public List<int> FindContactIds(int profileId, bool accepted)
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

        public Dictionary<ProfileModel, Category> FindContactsAndCategories(int profileId)
        {
            Dictionary<ProfileModel, Category> dictionary1 = Ctx.Contacts.Where(p => (p.ContactId == profileId)).
                    Select((x) => new { x.Profile, x.ProfileCategory }).ToDictionary(t => t.Profile, t => t.ProfileCategory);

            Dictionary<ProfileModel, Category> dictionary2 = Ctx.Contacts.Where(p => (p.ProfileId == profileId)).
                    Select((x) => new { x.Contact, x.ContactCategory }).ToDictionary(t => t.Contact, t => t.ContactCategory);

            var merged = dictionary1.Concat(dictionary2).ToLookup(x => x.Key, x => x.Value).ToDictionary(x => x.Key, g => g.First());

            return merged;
        }

        public void AcceptContact(int userProfileId, int contactId)
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

        public List<int> FindAllContacts(int id)
        {
            List<int> ids = new List<int>();

            ids.AddRange(Ctx.Contacts.Where((c) => (c.ProfileId == id)).
                    Select((x) => x.ContactId).
                    ToList());
            ids.AddRange(Ctx.Contacts.Where((c) => (c.ContactId == id)).
                    Select((x) => x.ProfileId).
                    ToList());

            return ids;
        }
    }
}