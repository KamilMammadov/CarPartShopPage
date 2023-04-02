using CarPartShop.Contracts.Email;

namespace CarPartShop.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
