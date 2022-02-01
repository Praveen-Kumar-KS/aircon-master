using Aircon.Business.Models.Customer.Contact;
using Aircon.Business.Models.Shared;
using Aircon.Data;
using Aircon.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services.Customer
{
    public interface ICustomerContactService
    {
        List<CustomerContactModel> GetCustomerContacts(int customerId, string searchText);
        CustomerContactModel GetCustomerContact(int contactId);
        CustomerContactModel UpdateContact(CustomerContactModel model);
        CustomerContactModel AddContact(CustomerContactModel model);
        void DeleteContact(int id);
    }


    public class CustomerContactService : ICustomerContactService
    {

        private readonly AirconDbContext _airconDbContext;

        public CustomerContactService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }
        public List<CustomerContactModel> GetCustomerContacts(int customerId,string searchText)
        {
            var customerContacts =  _airconDbContext.CustomerContacts.Where(x => x.Id == customerId)
                .Include(x => x.Contact)
                .Include(x => x.Address)
                .Select(x => new CustomerContactModel
                {
                    CustomerId = x.Id,
                    AddressId = x.AddressId,
                    ContactId = x.ContactId,
                    Contact = new ContactModel
                    {
                        FirstName = x.Contact.FirstName,
                        LastName = x.Contact.LastName,
                        CompanyName = x.Contact.CompanyName,
                        Title = x.Contact.Title,
                        Department = x.Contact.Department,
                        PhoneNumber = x.Contact.PhoneNumber,
                        Email = x.Contact.Email,
                        Active = x.Contact.Active,
                        SpecialInstruction = x.Contact.SpecialInstruction
                    },
                    Address = new AddressModel
                    {
                        NickName = x.Address.NickName,
                        Line1 = x.Address.Line1,
                        Line2 = x.Address.Line2,
                        City = x.Address.City,
                        State = x.Address.State,
                        Zip = x.Address.Zip,
                        IsActive = x.Address.IsActive,
                        Id = x.Address.Id
                    }
                }).ToList();
            if (searchText != null)
            {
                customerContacts =
                    customerContacts.Where(x =>
                         (x.Contact.FirstName == null ? false : x.Contact.FirstName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.LastName == null ? false : x.Contact.LastName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.CompanyName == null ? false : x.Contact.CompanyName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.Title == null ? false : x.Contact.Title.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.Email == null ? false : x.Contact.Email.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.PhoneNumber == null ? false : x.Contact.PhoneNumber.Contains(searchText)) ||
                         (x.Address.Line1 == null ? false : x.Address.Line1.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Address.Line2 == null ? false : x.Address.Line2.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Address.City == null ? false : x.Address.City.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Address.State == null ? false : x.Address.State.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Address.Zip == null ? false : x.Address.Zip.Contains(searchText))
                        ).Select(y => y).ToList();
            }
            return customerContacts;
        }

        public CustomerContactModel GetCustomerContact(int contactId)
        {
            return _airconDbContext.CustomerContacts
                .Where(x=> x.ContactId == contactId)
                .Include(x => x.Contact)
                .Include(x => x.Address)
                .Select(x => new CustomerContactModel
                {
                    CustomerId = x.Id,
                    AddressId = x.AddressId,
                    ContactId = x.ContactId,
                    Contact = new ContactModel
                    {
                        FirstName = x.Contact.FirstName,
                        LastName = x.Contact.LastName,
                        CompanyName = x.Contact.CompanyName,
                        Title = x.Contact.Title,
                        Department = x.Contact.Department,
                        PhoneNumber = x.Contact.PhoneNumber,
                        Email = x.Contact.Email,
                        Active = x.Contact.Active,
                        SpecialInstruction = x.Contact.SpecialInstruction
                    },
                    Address = new AddressModel
                    {
                        NickName = x.Address.NickName,
                        Line1 = x.Address.Line1,
                        Line2 = x.Address.Line2,
                        City = x.Address.City,
                        State = x.Address.State,
                        Zip = x.Address.Zip,
                        IsActive = x.Address.IsActive,
                        Id = x.Address.Id
                    }
                }).SingleOrDefault();
        }
        public CustomerContactModel AddContact(CustomerContactModel model)
        {

            CustomerContact customerContact = new CustomerContact
            {
                Id = model.CustomerId,
                Contact = new Contact
                {
                    FirstName = model.Contact.FirstName,
                    LastName = model.Contact.LastName,
                    Title = model.Contact.Title,
                    Email = model.Contact.Email,
                    PhoneNumber = model.Contact.PhoneNumber,
                    CompanyName = model.Contact.CompanyName,
                    SpecialInstruction = model.Contact.SpecialInstruction,
                    Department = model.Contact.Department,
                    Active = true
                },
                Address = new Address
                {
                    NickName = model.Address.NickName,
                    Line1 = model.Address.Line1,
                    Line2 = model.Address.Line2,
                    City = model.Address.City,
                    State = model.Address.State,
                    Zip = model.Address.Zip,
                    IsActive = true,
                }
            };
            _airconDbContext.CustomerContacts.Add(customerContact);
            _airconDbContext.SaveChanges();
            return model;
        }

        public CustomerContactModel UpdateContact(CustomerContactModel UpdateContactModel)
        {
            Contact updatecontacts = _airconDbContext.Contacts.Find(UpdateContactModel.ContactId);
            updatecontacts.FirstName = UpdateContactModel.Contact.FirstName;
            updatecontacts.LastName = UpdateContactModel.Contact.LastName;
            updatecontacts.Title = UpdateContactModel.Contact.Title;
            updatecontacts.CompanyName = UpdateContactModel.Contact.CompanyName;
            updatecontacts.Email = UpdateContactModel.Contact.Email;
            updatecontacts.PhoneNumber = UpdateContactModel.Contact.PhoneNumber;
            updatecontacts.SpecialInstruction = UpdateContactModel.Contact.SpecialInstruction;
            _airconDbContext.Contacts.Update(updatecontacts);
            Address updateaddress = _airconDbContext.Addresses.Find(UpdateContactModel.AddressId);
            updateaddress.NickName = UpdateContactModel.Address.NickName;
            updateaddress.Line1 = UpdateContactModel.Address.Line1;
            updateaddress.Line2 = UpdateContactModel.Address.Line2;
            updateaddress.City = UpdateContactModel.Address.City;
            updateaddress.State = UpdateContactModel.Address.State;
            updateaddress.Zip = UpdateContactModel.Address.Zip;
            updateaddress.IsActive = UpdateContactModel.Address.IsActive;
            _airconDbContext.Addresses.Update(updateaddress);
            _airconDbContext.SaveChanges();
            return UpdateContactModel;

        }
        public void DeleteContact(int id)
        {
             Contact contact = _airconDbContext.Contacts.Find(id);
            _airconDbContext.Contacts.Remove(contact);
            _airconDbContext.SaveChanges();
        }
    }
}
