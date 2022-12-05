using FinalDraft_11._6.Models;

namespace FinalDraft_11._6.Services;

public interface IStorage
{
    Session GetSession { get; set; }
}