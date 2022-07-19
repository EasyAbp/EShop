using System;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.FlashSales;

public class FullAuditedEntityEto<TPrimaryKey> : ExtensibleObject
{
    public TPrimaryKey Id { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public Guid? LastModifierId { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeleterId { get; set; }

    public DateTime? DeletionTime { get; set; }
}
