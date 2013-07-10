using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Models;

namespace CoolApp.Infraestructure.Data
{
    public partial class PreApprovalRepository : BaseRepository<PreApproval>, IPreApprovalRepository
    {
		public PreApprovalRepository(IDatabaseFactory databaseFactory)
	        : base(databaseFactory)
	    {
	    }    

      
    }
}