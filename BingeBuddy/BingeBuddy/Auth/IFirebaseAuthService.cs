using System.Threading.Tasks;
using BingeBuddy.Auth.Models;

namespace BingeBuddy.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}