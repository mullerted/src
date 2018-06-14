using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Neighborstash.Core.Models;
using Neighborstash.Core.Contracts;
using Neighborstash.Core.Repositories;
using Neighborstash.Core.ViewModels;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using AddressDetail = Neighborstash.Core.Models.AddressDetail;
using Assert = NUnit.Framework.Assert;
using Geo = Neighborstash.Core.Models.Geo;
using Phone = Neighborstash.Core.Models.Phone;
using UserLogin = Neighborstash.Core.Models.UserLogin;

namespace Neighborstash.Test
{
    public class BsonDocumentTest : TestBase
    {
        [Test]
        public void ShouldCreateEmptyBsonDocument()
        {

            var document = new BsonDocument();

            Console.WriteLine(document.ToJson());
            //same result
            Console.WriteLine(document);
            //Assert.IsNotNull(objectId);
        }

        [Test]
        public void ShouldCreateBsonDocument()
        {

            var person = new BsonDocument
            {
                {"age",new BsonInt32(54) }
            };
            person.Add("firstName", new BsonString("bob"));
            person.Add("lastname", new BsonString("michael"));

            Console.WriteLine(person);
            //Assert.IsNotNull(objectId);
        }

        [Test]
        public void ShouldAddArrays()
        {
            var person = new BsonDocument
            {
                {"age",new BsonInt32(54) }
            };
            person.Add("firstName", new BsonString("bob"));
            person.Add("lastname", new BsonString("michael"));

            person.Add("address",
                new BsonArray(new[] {"1010 cypress way", "apt. 900"}));
            person.Add("Gender", new BsonString("Male"));
            

            Console.WriteLine(person);
        }

        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldAddUserstoDb(string hostname,int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            for (var i = 0;  i < 10 ;i++)
            {
                var user = new User
                {
                    
                    Phones = new List<Phone>()
                    {
                        new Core.Models.Phone
                        {
                            Type = "home",
                            Num = $"{i}{i}{i}-{i}{i}{i}-{i}{i}{i}{i}",
                            IsPrefered = false
                             
                        },
                        new Core.Models.Phone
                        {
                            Type =  "work",
                            Num = $"{i*2}{i*2}{i*2}-{i}{i}{i}-{i}{i}{i}{i}",
                            IsPrefered = false
                        },
                        new Core.Models.Phone
                        {
                            Type = "cell",
                            Num = $"{i*3}{i*3}{i*3}-{i}{i}{i}-{i}{i}{i}{i}",
                            IsPrefered = true
                        }
                    },

                    DateOfBirth = "199{}-010-01",
                    
                    Identities = new List<SocIdentity>()
                    {
                        new SocIdentity
                        {
                            Provider = "google-oauth2",
                            UserId = $"101010010000000_{i}",
                            Connection = "google-oauth2",
                            IsSocial = true
                        }
                    },
                    Picture =
                        "https://secure.gravatar.com/avatar/5426f6b9d63ad92d60e6fe9fdf83aa21?s=480&r=pg&d=https%3A%2F%2Fssl.gstatic.com%2Fs2%2Fprofiles%2Fimages%2Fsilhouette80.png",
                    Createdate = DateTime.Now,
                    Email = $"hithm{i}@gmail.com",
                    Firstname = "Miller",
                    Lastname = "Teddie",
                    Username = $"hithm_{i}",
                    Gender = i%2 ==0 ? "Male" : "Female",
                    UserType = i%2 == 0 ? 0: 1, // neighbor=0, stasher=1
                    //Id = ObjectId.GenerateNewId().ToString()
                };

                nsContext.Users.InsertOne(user);


                var address = new UserAddress
                {
                    Username =  user.Username,
                    Location = i % 2 == 0 ? "home" : "work",

                    Address = new AddressDetail
                    {
                        Addr1 = $"{i * 100} Cypress way",
                        Addr2 = $"## {i}",
                        City = "Laurel",
                        State = "MD",
                        Pcode = i%2 ==0 ? "20724" : (20701 + i).ToString(),
                        Country = "USA"
                    },
                    Geo = new Geo()
                    {
                        Type = i % 2 == 0 ? "home" : "work",
                        Coord = new double[] {56.00000 + i*10, 45.000000 + i*5}
                    }

                };

                nsContext.UserAddresses.InsertOne(address);

                var userLogin = new UserLogin
                {
                    Username = user.Username,
                    Lastlogin = DateTime.Now,
                    Createdate = DateTime.Now,
                    Passwordhash = ConvertToSha256($"userpassword_{i}")

                };


                nsContext.UserLogins.InsertOne(userLogin);

                var secQuestionAnswer = new SecurityQeustion
                {
                     Username =  user.Username,
                     QuestionAnswers = new List<QuestionAnswer>()
                     {
                         new QuestionAnswer
                         {
                             Question = "Your first job", Answer = "DOI"

                         },
                         new QuestionAnswer
                         {
                             Question = "Your first pet name", Answer = 
                                 "Tina"
                         },
                         new QuestionAnswer
                         {
                             Question = "Your first car model",
                             Answer = "Dahtsun"
                         }
                     }
                };

               var result = nsContext.UserSecurityQuestions.InsertOneAsync(secQuestionAnswer);
                Console.WriteLine($"sercQuestion ID: {result.Id}");
               
                
            }


        }

        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldAddNeighborStasher(string hostname, int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            for (var i = 0; i < 10; i++)
            {
                var mystashers = new NeighborStasher
                {
                    Username = $"hithm_{i}",

                    Stashers = new List<Stasher>
                    {
                        new Stasher{ Username = $"hithm_{i+1}", Rank =1 },
                        new Stasher{ Username = $"hithm_{i+2}", Rank =2 },
                        new Stasher{ Username = $"hithm_{i+3}", Rank =3 }
                    }
                };

                nsContext.NeighborStasher.InsertOne(mystashers);

            }
        }
        private static int GetStasherId(int i)
        {
            var random = new Random();
            var randNum = random.Next(0, 10);
            var stasherId = i == randNum ? i++ : randNum;
            return stasherId;
        }

        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldUpdateUser(string hostname, int portNum, string databaseName)
        {
                var update = new UpdateDefinitionBuilder<User>();
            update.Set("Phone", "5132359201");

            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });


          
           // UpdateDefinition<User> upd ="{Phone:5132359206}";
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(u => u.Lastname, "Teddie");

            var ub = Builders<User>.Update;

            UpdateDefinition<User> updater = new BsonDocumentUpdateDefinition<User>(new BsonDocument
            {
                {"Locale","En" }
            });

           
            var filterDefinition = Builders<User>.Filter.Empty;
            filterDefinition &= Builders<User>.Filter.Eq(u => u.Email, "hithm1@gmail.com");
            var qFind =  nsContext.Users.Find(filterDefinition);
            var user = qFind.First();
            Console.WriteLine($"{user.Lastname}, {user.Firstname}");

             var result = nsContext.Users.UpdateOne(u => u.Email == "hithm2@gmail.com", updater, null);

            Assert.IsTrue(result.IsAcknowledged);

            //var modification = new UpdateDefinition<User>().Push(u => u.Lastname, "").Set(r => r.DateOfBirth, "");

        }

        [TestCase("localhost", 27017, "Neighborstash", "c:\\temp\\brain.png")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task UploadFileAsync(string hostname, int portNum, string databaseName, string filename)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });


            var bucket = new GridFSBucket(nsContext.Database);
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("contentType", "image/png")
            };

            using (var fs = new FileStream(filename, FileMode.Open))
            {
               var imageId = await bucket
                    .UploadFromStreamAsync(filename, fs); 
                
                Console.WriteLine(imageId);
            }

           
            
        }



        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task MongoDriverReading(string hostname, int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            var bi = new BsonDocument("buildinfo", 1);


            var buildInfo = nsContext.Database.RunCommandAsync<BsonDocument>(bi);
            var bInnfo = GetServerInfoAsync(nsContext);

            Console.WriteLine(bInnfo.Result);
            // Console.WriteLine(buildInfo.Result);

            //var gf =   nsContext.StoreImageAsync(@"C:\Users\mhadish1\Pictures\adl-2.jpg");

            //Console.WriteLine($"imageid: {gf.Result.ToString()}");

            var nsUsers = nsContext.Users.Find(new BsonDocument
                   {
                       { "Email", "hithm5@gmail.com"}
                   }

                )
                 .ToList();


            nsUsers = nsContext.Users.Find(Builders<User>.Filter.Eq(u => u.Email, "hithm1@gmail.com")
               )
               .ToList();

            nsUsers = nsContext.Users.Find(Builders<User>.Filter.Where(u => u.Email == "hithm1@gmail.com")
                )
                .ToList();
            ;
            var filterDefinition = Builders<User>.Filter.Where(u => u.Email == "hithm1@gmail.com");

            filterDefinition = Builders<User>.Filter.Ne(u => u.Email, "hithm1@gmail.com");



            var nsUsersAync = await nsContext.Users.Find(filterDefinition)
                //.Sort(Builders<User>.Sort.Ascending(r=>r.Lastname))
                .SortBy(u => u.Lastname)
                .ThenByDescending(u=>u.Firstname)
               .ToListAsync();

            Console.WriteLine($"filter in bson doc \n {filterDefinition.Render(BsonSerializer.SerializerRegistry.GetSerializer<User>(), BsonSerializer.SerializerRegistry)}");


            foreach (var nsUser in nsUsers)
            {
                Console.WriteLine($"{nsUser.Lastname}, {nsUser.Firstname} - {nsUser.Email}");
            }
        }


        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task MongoDriverCreating(string hostname, int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            var secQuestionAnswer = new SecurityQeustion
            {
                Username = "hithm_10", //user.Username,
                QuestionAnswers = new List<QuestionAnswer>()
                {
                    new QuestionAnswer
                    {
                        Question = "Your fav movie",
                        Answer = "God Father"

                    },
                    new QuestionAnswer
                    {
                        Question = "Your firts international travel destination",
                        Answer =
                            "Morituass"
                    },
                    new QuestionAnswer
                    {
                        Question = "Your first girl friend",
                        Answer = "Hanna"
                    }
                }
            };

            // insert async
            await nsContext.UserSecurityQuestions.InsertOneAsync(secQuestionAnswer);

            // or
            var result = nsContext.UserSecurityQuestions.InsertOneAsync(secQuestionAnswer);
            Console.WriteLine($"sercQuestion ID: {result.Id}");


            var sc = nsContext.UserSecurityQuestions
                .Find(Builders<SecurityQeustion>.Filter.Where(s => s.Username == "hithm_0"))
                .FirstOrDefault();

            sc = nsContext.UserSecurityQuestions
                .Find(s => s.Username == "hithm_0")
                .FirstOrDefault();

            // now modify or update
            sc.QuestionAnswers.Add( new QuestionAnswer
            {
                Question = "What's your maternal grandmother's name",
                Answer = "Dmk"
            });

            if (sc.Username == "hithm_0")
            {
                var replaced = await nsContext.UserSecurityQuestions.ReplaceOneAsync(s => s.Username == "hithm_0", sc);
                Console.WriteLine(replaced.IsAcknowledged);
            }
            
        }

        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task MongoDriverReplace(string hostname, int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            //var sc = nsContext.UserSecurityQuestions
            //    .Find(Builders<SecurityQeustion>.Filter.Where(s => s.Username == "hithm_0"))
            //    .FirstOrDefault();

            // or 
            var sc = nsContext.UserSecurityQuestions
                .Find(s => s.Username == "hithm_0")
                .FirstOrDefault();

            // now modify or update
            var qs = new QuestionAnswer
            {
                Question = "What's your maternal grandmother's name",
                Answer = "Dmk"
            };

            //sc.QuestionAnswers[sc.QuestionAnswers.Length + 1]

            if (sc.Username == "hithm_0")
            {
                var replaced = await nsContext.UserSecurityQuestions.ReplaceOneAsync(s => s.Username == "hithm_0", sc);
                Console.WriteLine(replaced.IsAcknowledged);
            }


        }

        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task MongoDriverUpdating(string hostname, int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            //var sc = nsContext.UserSecurityQuestions
            //    .Find(Builders<SecurityQeustion>.Filter.Where(s => s.Username == "hithm_0"))
            //    .FirstOrDefault();

            // or 
            var user = nsContext.Users.Find(r => r.Username == "hithm_0").FirstOrDefault();

            if (user.Username == "hithm_0")
            {
                var modificationUpdate = Builders<User>.Update
                        //.Push()
                    .Set(r => r.Firstname, "Mollykahan");
                nsContext.Users.UpdateOne(u => u.Username == "hithm_0", modificationUpdate);

                
            }


        }


        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task MongoDriverProjecting(string hostname, int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            //var sc = nsContext.UserSecurityQuestions
            //    .Find(Builders<SecurityQeustion>.Filter.Where(s => s.Username == "hithm_0"))
            //    .FirstOrDefault();

            // or 
            var user = nsContext.Users
                .Find(r => r.Username == "hithm_0")
                .Project(r=> new UserDetailViewModel
                {
                    UserEmail = r.Username,
                    FirstName = r.Firstname,
                    LastName = r.Lastname,
                    UserName = r.Username
                })
                .FirstOrDefault();

            Console.WriteLine(user.ToString());

        }


        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task MongoDriverUpserting(string hostname, int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            //var sc = nsContext.UserSecurityQuestions
            //    .Find(Builders<SecurityQeustion>.Filter.Where(s => s.Username == "hithm_0"))
            //    .FirstOrDefault();

            // or 
            var user = nsContext.Users.Find(r => r.Username == "hithm_0").FirstOrDefault();
            UpdateOptions options = new UpdateOptions
            {
                IsUpsert = true
            };

            if (user.Username == "hithm_0")
            {
                var modificationUpdate = Builders<User>.Update
                    //.Push()
                    .Set(r => r.Firstname, "Obama");
                nsContext.Users.UpdateOne(u => u.Username == "hithm_0", modificationUpdate, options);


            }


        }


        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task MongoDriverDeleting(string hostname, int portNum, string databaseName)
        {
            var nsContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            // delete all users
            for (var i = 0; i < 10; i++)
            {
                var uname = $"hithm_{i}";
                nsContext.Users.DeleteOne(u => u.Username == uname, CancellationToken.None);
                nsContext.UserAddresses.DeleteOne(u => u.Username == uname, CancellationToken.None);
                nsContext.UserLogins.DeleteOne(u => u.Username == uname, CancellationToken.None);
            }

        }


       

    }


}


