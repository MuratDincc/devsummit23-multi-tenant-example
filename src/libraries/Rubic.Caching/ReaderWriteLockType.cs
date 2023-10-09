namespace Rubic.Caching;

/// <summary>
/// Reader/Write locker type
/// </summary>
public enum ReaderWriteLockType
{
    Read,
    Write,
    UpgradeableRead
}
