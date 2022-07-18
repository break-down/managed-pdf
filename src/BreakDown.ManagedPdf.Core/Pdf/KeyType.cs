﻿using System;

namespace BreakDown.ManagedPdf.Core.Pdf
{
    /// <summary>
    /// Specifies the type of a key's value in a dictionary.
    /// </summary>
    [Flags]
    internal enum KeyType
    {
        Name = 0x00000001,
        String = 0x00000002,
        Boolean = 0x00000003,
        Integer = 0x00000004,
        Real = 0x00000005,
        Date = 0x00000006,
        Rectangle = 0x00000007,
        Array = 0x00000008,
        Dictionary = 0x00000009,
        Stream = 0x0000000A,
        NumberTree = 0x0000000B,
        Function = 0x0000000C,
        TextString = 0x0000000D,
        ByteString = 0x0000000E,
        NameTree = 0x0000000F,
        FileSpecification = 0x00000010,

        NameOrArray = 0x00000100,
        NameOrDictionary = 0x00000200,
        ArrayOrDictionary = 0x00000300,
        StreamOrArray = 0x00000400,
        StreamOrName = 0x00000500,
        ArrayOrNameOrString = 0x00000600,
        FunctionOrName = 0x000000700,
        Various = 0x000000800,

        TypeMask = 0x00000FFF,

        Optional = 0x00001000,
        Required = 0x00002000,
        Inheritable = 0x00004000,
        MustBeIndirect = 0x00010000,
        MustNotBeIndirect = 0x00020000,
    }
}
