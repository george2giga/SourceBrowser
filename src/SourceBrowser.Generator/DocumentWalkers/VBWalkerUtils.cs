using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using SourceBrowser.Generator.Model.VisualBasic;

namespace SourceBrowser.Generator.DocumentWalkers
{
    class VBWalkerUtils : VisualBasicSyntaxWalker, IWalkerUtils
    {
        private readonly DocumentWalker<VBWalkerUtils> _walker;

        internal VBWalkerUtils(DocumentWalker<VBWalkerUtils> walker)
             : base(SyntaxWalkerDepth.Trivia)
        {
            _walker = walker;
        }

        public string IdentifierTokenTypeName
        {
            get { return VisualBasicTokenTypes.IDENTIFIER; }
        }

        public string KeywordTokenTypeName
        {
            get { return VisualBasicTokenTypes.KEYWORD; }
        }

        public string OtherTokenTypeName
        {
            get { return VisualBasicTokenTypes.OTHER; }
        }

        public string StringTokenTypeName
        {
            get { return VisualBasicTokenTypes.STRING; }
        }

        public string TypeTokenTypeName
        {
            get { return VisualBasicTokenTypes.TYPE; }
        }

        public string ParameterDelimiter
        {
            get { return VBDelimiters.PARAMETER; }
        }

        public string LocalVariableDelimiter
        {
            get { return VBDelimiters.LOCAL_VARIABLE; }
        }


        public string GetFullName(SyntaxToken token)
        {
            return token.CSharpKind().ToString();
        }

        public bool IsIdentifier(SyntaxToken token)
        {
            return token.VBKind() == SyntaxKind.IdentifierToken;
        }

        public bool IsKeyword(SyntaxToken token)
        {
            return token.IsKeyword();
        }

        public bool IsLiteral(SyntaxToken token)
        {
            return token.VBKind() == SyntaxKind.StringLiteralToken;
        }

        public override void VisitToken(SyntaxToken token)
        {
            _walker.VisitToken(token);
        }
    }
}
