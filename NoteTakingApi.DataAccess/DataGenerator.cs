using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoteTakingApi.Common.Hellpers;
using NoteTakingApi.DataAccess.DataContexts;
using NoteTakingApi.DataAccess.Entities;
using System;
using System.Linq;

namespace NoteTakingApi.DataAccess
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context =
                new NoteTakingDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<NoteTakingDbContext>>());

            if (context.User.Any())
                return;

            context.User.Add(new User
            {
                Username = "test",
                Password = EncryptionHelper.EncryptPassword("test", "z^Z~ML")
            });

            context.Note.Add(new Note
            {
                Content = "First note ever!",
                Title = "First note",
                UserId = 1
            });

            context.SaveChanges();
        }
    }
}
