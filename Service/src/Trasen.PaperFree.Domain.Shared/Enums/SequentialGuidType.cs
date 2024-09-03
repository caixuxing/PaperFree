namespace Trasen.PaperFree.Domain.Shared.Enums
{
    public enum SequentialGuidType
    {
        //
        // 摘要:
        //     The GUID should be sequential when formatted using the Guid.ToString() method.
        //     Used by MySql and PostgreSql.
        SequentialAsString,

        //
        // 摘要:
        //     The GUID should be sequential when formatted using the Guid.ToByteArray method.
        //     Used by Oracle.
        SequentialAsBinary,

        //
        // 摘要:
        //     The sequential portion of the GUID should be located at the end of the Data4
        //     block. Used by SqlServer.
        SequentialAtEnd
    }
}