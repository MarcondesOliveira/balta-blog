using System;
using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace blog
{
    class Program
    {
        private const string CONNECTION_STRING = "Server=localhost,1433;Database=BLOG;User ID=sa;Password=@teste@123";
        static void Main(string[] args)
        {
            var connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();

            ReadUsersWithRoles(connection);
            // ReadUsers(connection);
            // CreateUser(connection);
            // ReadRoles(connection);
            // ReadTags(connection);
            // UpdateUser(connection);
            // UpdateRole(connection);
            // UpdateTag(connection);
            // DeleteUser(connection);

            connection.Close();
        }

        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);
            var items = repository.Get();

            foreach (var item in items)
            {

                Console.WriteLine(item.Name);
                foreach (var role in item.Roles)
                {
                    Console.WriteLine($" - {role.Name}");
                }
            }

        }

        public static void CreateUser(SqlConnection connection)
        {
            var user = new User()
            {
                Email = "gato@gato.com.br",
                Bio = "bio",
                Image = "https://...",
                Name = "Gato Troll",
                PasswordHash = "Hash",
                Slug = "gato-troll"
            };
            var repository = new Repository<User>(connection);
            repository.Create(user);
        }

        public static void ReadUsersWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var items = repository.GetWithRoles();

            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
                foreach (var role in item.Roles)
                {
                    Console.WriteLine($" - {role.Name}");
                }
            }
        }

        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new Repository<Role>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);

        }

        public static void ReadTags(SqlConnection connection)
        {
            var repository = new Repository<Tag>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);

        }

        public static void UpdateUser(SqlConnection connection)
        {
            var user = new User()
            {
                Id = 1,
                Bio = "Programador Frontend",
                Email = "marcondes@marcondes.com.br",
                Image = "https://...",
                Name = "Marcondes A Oliveira",
                PasswordHash = "Hash",
                Slug = "marcondes-oliveira"
            };

            connection.Update<User>(user);

            Console.WriteLine("Atualização realizada com sucesso!");
        }

        public static void UpdateRole(SqlConnection connection)
        {
            var role = new Role()
            {
                Id = 1,
                Name = "Autor Marcondes",
                Slug = "author-marcondes"
            };

            connection.Update<Role>(role);

            Console.WriteLine("Atualização realizada com sucesso!");
        }

        public static void UpdateTag(SqlConnection connection)
        {
            var tag = new Tag()
            {
                Id = 1,
                Name = "ASP.Net",
                Slug = "asp-net"
            };

            connection.Update<Tag>(tag);

            Console.WriteLine("Atualização realizada com sucesso!");
        }

        public static void DeleteUser(SqlConnection connection)
        {
            var user = connection.Get<User>(3);
            connection.Delete<User>(user);

            Console.WriteLine("Exclusão realizada com sucesso!");
        }
    }
}
