using System;
using System.Collections.Generic;
using System.Globalization;

namespace PhoneContactList
{
    class Contact
    {
        public string Name { get; set; }
        public long Phone { get; set; }
    }

    class Program
    {
        public static void DisplayListAll(List<Contact> myList)
        {
            if (myList.Count == 0)
            {
                Console.WriteLine("List is empty!");
            }
            else
            {
                Console.WriteLine("\nAll Contacts from the phonebook: \n");
                foreach (var item in myList)
                {
                    Console.WriteLine("Name: {0}, Phone: {1}", item.Name, item.Phone);
                }
            }
        }

        public static string ConvertToNameCase(string name)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(name.ToLower());
        }

        public static void DisplayAllContactsOfAUserName(List<Contact> myList, string name)
        {
            Console.WriteLine($"\nAll Contacts of {name}:");

            int contactNo = 1;
            foreach (var contact in myList)
            {
                if (contact.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Contact No {contactNo++}: {contact.Phone}");
                }
            }
        }

        public static Contact FindContactGivenAPhone(List<Contact> myList, long phone)
        {
            foreach (var contact in myList)
            {
                if (contact.Phone == phone)
                {
                    return contact;
                }
            }
            return null;
        }

        public static List<Contact> FindAllContactsOfAGivenName(List<Contact> myList, string name)
        {
            List<Contact> foundContacts = new List<Contact>();
            foreach (var contact in myList)
            {
                if (contact.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    foundContacts.Add(contact);
                }
            }
            return foundContacts;
        }

        static void Main(string[] args)
        {
            List<Contact> contactList = new List<Contact>();
            string command;
            do
            {
                Console.WriteLine("\nEnter a command: ");
                command = Console.ReadLine().Trim().ToLower();
                var split = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 0)
                {
                    continue;
                }
                if (split[0] == "exit")
                {
                    break;
                }
                switch (split[0])
                {
                    case "findall":
                        DisplayListAll(contactList);
                        break;
                    case "add":
                        if (split.Length >= 3)
                        {
                            string nameToAdd = ConvertToNameCase(string.Join(" ", split, 1, split.Length - 2));
                            long phoneToAdd;
                            if (long.TryParse(split[split.Length - 1], out phoneToAdd))
                            {
                                contactList.Add(new Contact { Name = nameToAdd, Phone = phoneToAdd });
                            }
                            else
                            {
                                Console.WriteLine("Invalid phone number format.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid add command format.");
                        }
                        break;
                    case "delete":
                        if (split.Length >= 2)
                        {
                            long phoneToDelete;
                            if (long.TryParse(split[1], out phoneToDelete))
                            {
                                Contact contactToDelete = FindContactGivenAPhone(contactList, phoneToDelete);
                                if (contactToDelete != null)
                                {
                                    contactList.Remove(contactToDelete);
                                    Console.WriteLine("Contact deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Contact not found with the given phone number.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid phone number format.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid delete command format.");
                        }
                        break;
                    case "find":
                        if (split.Length >= 2)
                        {
                            string nameToFind = ConvertToNameCase(string.Join(" ", split, 1, split.Length - 1));
                            DisplayAllContactsOfAUserName(contactList, nameToFind);
                        }
                        else
                        {
                            Console.WriteLine("Invalid find command format.");
                        }
                        break;
                    case "update":
                        if (split.Length >= 4)
                        {
                            int lastIdx = split.Length - 1;
                            long newPhone;
                            if (long.TryParse(split[lastIdx], out newPhone))
                            {
                                string updatedName = ConvertToNameCase(string.Join(" ", split, 2, lastIdx - 2));
                                long oldPhone;
                                if (long.TryParse(split[1], out oldPhone))
                                {
                                    Contact contactToUpdate = FindContactGivenAPhone(contactList, oldPhone);
                                    if (contactToUpdate != null)
                                    {
                                        contactToUpdate.Name = updatedName;
                                        contactToUpdate.Phone = newPhone;
                                        Console.WriteLine($"Contact updated successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Contact not found with the given old phone number.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid old phone number format.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid new phone number format.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid update command format.");
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown command! \nPlease enter command in correct format...");
                        break;
                }
            } while (true);
        }
    }
}