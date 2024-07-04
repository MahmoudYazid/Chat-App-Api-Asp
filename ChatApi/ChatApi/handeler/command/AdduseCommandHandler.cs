using ChatApi.model.command;
using ChatApi.model.dbModel;
using MediatR;
using MongoDB.Driver;

namespace ChatApi.handeler.command
{
    public class AdduseCommandHandler : IRequestHandler<AddUserCommand, String>
    {
        public MongoDbService _MongoDbService { get; set; }
        public AdduseCommandHandler(MongoDbService mongoDbServiceInjected) { 
            
            _MongoDbService = mongoDbServiceInjected;
        }

        public async Task<String> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            PersonDbModel Newperson = new PersonDbModel { 
            
                Name = request.Name,
                password = request.password,
                Age = request.Age
            };
            var Filter= Builders<PersonDbModel>.Filter.Eq("Name", request.Name);
            var result = await _MongoDbService.PersonDb.Find(Filter).FirstOrDefaultAsync();
            if (result != null)
            {
                return "that user exist before" ;
            }
            else {
                await _MongoDbService.PersonDb.InsertOneAsync(Newperson);
                return "user has been created";
            }
           

           
           
        }
    }
}
