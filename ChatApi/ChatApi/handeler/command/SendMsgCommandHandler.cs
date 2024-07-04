using ChatApi.model.command;
using ChatApi.model.dbModel;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Claims;

namespace ChatApi.handeler.command
{
    public class SendMsgCommandHandler : IRequestHandler<SendMsgCommand, String>
    {
        public MongoDbService _MongoDbService { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public SendMsgCommandHandler (MongoDbService mongoDbServiceInjected , IHttpContextAccessor httpContextAccessorInjected)
        {

            _MongoDbService = mongoDbServiceInjected;
            this.HttpContextAccessor = httpContextAccessorInjected;
        }

        

        public async Task<string> Handle(SendMsgCommand request, CancellationToken cancellationToken)
        {
            var NewMsgInst = new MsgDbModel();
            if (request.VideoMsg != null) {
                var fileExtention= Path.GetExtension(request.VideoMsg.FileName);
                var FolderPath = "C:\\Users\\ahmed\\Desktop\\GIT\\Chat-App-Api-Asp\\ChatApi\\ChatApi\\images\\";
                var fileName_WithoutExtention = Path.GetFileNameWithoutExtension(request.VideoMsg.FileName);
                var filter_ = Builders<MsgDbModel>.Filter.Empty; 
                long count = await _MongoDbService.MsgsDb.CountDocumentsAsync(filter_);
                fileName_WithoutExtention = fileName_WithoutExtention +  count.ToString() + fileExtention;
                var FullPath = Path.Combine(FolderPath, fileName_WithoutExtention);

                using (var stream = new FileStream(FullPath, FileMode.Create))
                {
                    await stream.CopyToAsync(stream);



                }


                //add video to Instance
                NewMsgInst.VideoMsg = FullPath;


            }
            else
            {


                NewMsgInst.VideoMsg = "";
            }

            NewMsgInst.TextMsg = request.TextMsg;

            // make sure that both Ids exists
            var currentUser = HttpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (!currentUser.IsAuthenticated) {

                return "you should login first"; 
            }
            var FilterForSender = Builders<PersonDbModel>.Filter.Eq("id", currentUser.Name.ToString());
            var CurrentUserQuery = _MongoDbService.PersonDb.Find(FilterForSender).FirstOrDefaultAsync();
            if(CurrentUserQuery == null || FilterForSender == null)
            {
                return "you should login first to send";
            }

            var FilterForReciever = Builders<PersonDbModel>.Filter.Eq("id",request.recieverId);
            var RecieverUserQuery = _MongoDbService.PersonDb.Find(FilterForReciever).FirstOrDefaultAsync();
            if (RecieverUserQuery == null)
            {
                return "the reciever is not exist";
            }
            NewMsgInst.recieverId = request.recieverId;
            NewMsgInst.senderId = currentUser.Name.ToString();

            await _MongoDbService.MsgsDb.InsertOneAsync(NewMsgInst);

            return "msg has been sent";


        }
    }
}
