using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SourceBrowser.Generator.Model.CSharp;

namespace SourceBrowser.Generator.DocumentWalkers
{
    internal class CSWalkerUtils : CSharpSyntaxWalker, IWalkerUtils
    {
        private readonly DocumentWalker<CSWalkerUtils> _walker;

        internal CSWalkerUtils(DocumentWalker<CSWalkerUtils> walker)
            : base(SyntaxWalkerDepth.Trivia)
        {
            _walker = walker;
        }

        public string IdentifierTokenTypeName
        {
            get { return CSharpTokenTypes.IDENTIFIER; }
        }

        public string KeywordTokenTypeName
        {
            get { return CSharpTokenTypes.KEYWORD; }
        }

        public string OtherTokenTypeName
        {
            get { return CSharpTokenTypes.OTHER; }
        }

        public string StringTokenTypeName
        {
            get { return CSharpTokenTypes.STRING; }
        }

        public string TypeTokenTypeName
        {
            get { return CSharpTokenTypes.TYPE; }
        }

        public string ParameterDelimiter
        {
            get { return CSharpDelimiters.PARAMETER; }
        }

        public string LocalVariableDelimiter
        {
            get { return CSharpDelimiters.LOCAL_VARIABLE; }
        }

        public string GetFullName(SyntaxToken token)
        {
            return token.Text;

            //return token.CSharpKind().ToString();
        }

        public bool IsIdentifier(SyntaxToken token)
        {
            return token.CSharpKind() == SyntaxKind.IdentifierToken;
        }

        public bool IsKeyword(SyntaxToken token)
        {
            return token.IsKeyword();
        }

        public bool IsLiteral(SyntaxToken token)
        {
            return token.CSharpKind() == SyntaxKind.StringLiteralToken;
        }

        public override void VisitToken(SyntaxToken token)
        {
            _walker.VisitToken(token);
        }
    }
}
