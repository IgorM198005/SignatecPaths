using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SignatecPaths
{
    internal sealed class SgtPath
    {
        private const string absPathPattern = "^/([A-Za-z]+(/[A-Za-z]+)*)?$";

        public const string InvalidArgumentFormatMessage = "Неверный формат аргумента";

        public const string InvalidArgumentNestingMessage = "Уровень вложенности заданного аргументом превышает текущий";
       
        public SgtPath(string path)
        {
            if (!Regex.IsMatch(path, SgtPath.absPathPattern))
            {
                throw new ArgumentException("Неверный формат пути");
            }

            this.mCurrentPath = path;
        }

        private string mCurrentPath;

        public string Cd(string cmd)
        {
            Match m;

            if ((m = Regex.Match(cmd, @"^(?<up>\.\.(/\.\.)*)(?<newPath>(/[A-Za-z]+)*)$")).Success)
            {
                this.CombineUpPath(m);
            }
            else if ((m = Regex.Match(cmd, @"^(?<this>\.)(?<newPath>(/[A-Za-z]+)+)?$")).Success)
            {
                this.CombineDownPath(m);
            }
            else if (Regex.IsMatch(cmd, SgtPath.absPathPattern))
            {
                this.mCurrentPath = cmd;
            }
            else
            {
                throw new ArgumentException(SgtPath.InvalidArgumentFormatMessage);
            }            

            return this.mCurrentPath;
        }

        private void CombineUpPath(Match m)
        {
            var segments = this.mCurrentPath == "/" ? new string[] { string.Empty } : 
                Regex.Split(this.mCurrentPath, "/");

            int upToIdx = segments.Length -1 - Regex.Matches(m.Groups["up"].Value, "..").Count;

            if (upToIdx < 0) throw new ArgumentException(SgtPath.InvalidArgumentNestingMessage);

            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= upToIdx; i++)
            {
                sb.Append("/");

                sb.Append(segments[i]);
            }

            sb.Append(m.Groups["newPath"].Value);

            this.mCurrentPath =  sb.ToString();

            if (string.IsNullOrEmpty(this.mCurrentPath)) this.mCurrentPath = "/";
        }

        private void CombineDownPath(Match m)
        {
            if (m.Groups["newPath"].Success)
            {
                if (this.mCurrentPath == "/")
                {
                    this.mCurrentPath = m.Groups["newPath"].Value;
                }
                else
                {
                    this.mCurrentPath += m.Groups["newPath"].Value;
                }
            }
        }
    }
}
