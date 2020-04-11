using System;

namespace RougeLikeRpg.Graphic.Core 
{
    public struct AsciiSymbol
    {
        ///<summary>
        ///  Символьное представление
        ///</summary>
        public char Symbol { get; private set; }
        ///<summary>
        ///  Код символа 
        ///</summary>
        public UInt16 Code { get; private set; }


        public AsciiSymbol(char Symbol) 
            : this (Symbol, (UInt16)Symbol) {}

        public AsciiSymbol(UInt16 Code) 
            : this ((char)Code, Code) {}

        public AsciiSymbol (char Symbol, UInt16 Code)
        {
            this.Symbol = Symbol;
            this.Code = Code;
        }

        public static implicit operator AsciiSymbol(char sym)
            => new AsciiSymbol(sym);

        public static implicit operator AsciiSymbol(UInt16 code)
            => new AsciiSymbol(code);

        public override bool Equals(object other)
        {
            if (other is char character)
                return character == Symbol;
            else if (other is UInt16 code)
                return code == this.Code;
            else
                throw new Exception("Type Error");
        }
    }


    public class AsciiSymbolManager 
    {
    }
}
