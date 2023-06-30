using OrderManagement.Core.Enums;

namespace OrderManagement.Core.Entities.Abstractions;

/// <summary>
/// Base in all entities must have these.
/// </summary>
public abstract class BaseEntity
{
    protected BaseEntity()
    {
        IsDeleted = false;
        StatusRecord = StatusRecord.Active;
    }

    public string? CreatedBy { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? CreatedAtServer { get; set; }

    public string? LastUpdatedBy { get; set; }
    public string? LastUpdatedByName { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public DateTime? LastUpdatedAtServer { get; set; }

    /// <summary>
    /// Default value is false.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Default value is <see cref="Enums.StatusRecord.Active">Active</see>
    /// </summary>
    public StatusRecord StatusRecord { get; set; }

    public void SetStatusRecordToInActive()
    {
        StatusRecord = StatusRecord.InActive;
    }

    public void SetStatusRecordToActive()
    {
        StatusRecord = StatusRecord.Active;
    }

    public void SetToDeleted()
    {
        SetStatusRecordToInActive();
        IsDeleted = true;
    }

    public void ModifyLastUpdated(string? lastUpdateBy, string? lastUpdateByName, DateTime? lastUpdateAt)
    {
        LastUpdatedBy = lastUpdateBy;
        LastUpdatedByName = lastUpdateByName;
        LastUpdatedAt = lastUpdateAt;
    }
}