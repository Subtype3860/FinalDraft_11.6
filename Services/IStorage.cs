using FinalDraft_11._6.Models;

namespace FinalDraft_11._6.Services
{
    internal interface IStorage
    {
        public Session GetSession(long chatId);
    }
}
