using MediatR;
using System;

namespace Seje.OrdenCaptura.SharedKernel
{
    public class SejeCommand<TResponse> : IRequest<TResponse>
    {
        public string UserName { get; private set; }

        public SejeCommand(string userName)
        {
            this.UserName = userName ?? throw new ArgumentNullException(nameof(UserName));
        }
    }
}
