#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Core
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

namespace CoolApp.Core.Interfaces.Data
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}