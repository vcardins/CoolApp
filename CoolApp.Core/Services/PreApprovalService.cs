using System.Linq;
using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Models;

namespace CoolApp.Core.Services
{
    public partial class PreApprovalService : BaseService<PreApproval>, IPreApprovalService
    {
		private readonly IPreApprovalRepository _preApprovalRepository;
		
		public PreApprovalService(IUnitOfWork unitOfWork, IPreApprovalRepository preApprovalRepository)
			:base(unitOfWork)
		{
            Repository = _preApprovalRepository = preApprovalRepository;
		}
    }
}