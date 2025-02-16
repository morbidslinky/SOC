using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class LuaLexer
    {
        private enum State
        {
            Normal,
            SingleLineComment,
            MultiLineComment,
            StringLiteral
        }

        private static List<string> Tokenize(string luaCode)
        {
            List<string> tokens = new List<string>();
            State state = State.Normal;
            StringBuilder tokenBuilder = new StringBuilder();
            bool awaitingCloseBracket = false;

            for (int i = 0; i < luaCode.Length; i++)
            {
                char current = luaCode[i];
                char next = (i + 1 < luaCode.Length) ? luaCode[i + 1] : '\0';

                switch (state)
                {
                    case State.Normal:
                        if (current == '-' && next == '-')
                        {
                            state = State.SingleLineComment;
                            tokenBuilder.Append("--");
                            i++;
                        }
                        else if (current == '[' && next == '[')
                        {
                            state = State.StringLiteral;
                            awaitingCloseBracket = true;
                            tokenBuilder.Append("[[");
                            i++;
                        }
                        else if (current == '"' || current == '\'')
                        {
                            state = State.StringLiteral;
                            awaitingCloseBracket = false;
                            tokenBuilder.Append(current);
                        }
                        else if (char.IsWhiteSpace(current))
                        {
                            if (current == '\n')
                            {
                                if (tokenBuilder.Length > 0)
                                {
                                    tokens.Add(tokenBuilder.ToString());
                                    tokenBuilder.Clear();
                                }
                                tokens.Add("\n");
                            }
                            else if (tokenBuilder.Length > 0)
                            {
                                tokens.Add(tokenBuilder.ToString());
                                tokenBuilder.Clear();
                            }
                        }
                        else
                        {
                            tokenBuilder.Append(current);
                        }
                        break;

                    case State.SingleLineComment:
                        var prev = luaCode[i - 1];
                        var prevPrev = luaCode[i - 2];
                        if (prevPrev == '-' && prev == '-' && current == '[' && next == '[')
                        {
                            state = State.MultiLineComment;
                            awaitingCloseBracket = true;
                            tokenBuilder.Append("[[");
                            i += 1;
                        }
                        else if (current == '\n')
                        {
                            state = State.Normal;
                            tokenBuilder.Append(current); 
                            tokens.Add(tokenBuilder.ToString());
                            tokenBuilder.Clear();
                        }
                        else
                        {
                            tokenBuilder.Append(current);
                        }
                        break;

                    case State.MultiLineComment:
                        if (current == ']' && next == ']' && awaitingCloseBracket)
                        {
                            state = State.Normal;
                            awaitingCloseBracket = false;
                            tokenBuilder.Append("]]");
                            tokens.Add(tokenBuilder.ToString());
                            tokenBuilder.Clear();
                            i++;
                        } 
                        else
                        {
                            tokenBuilder.Append(current);
                        }
                        break;

                    case State.StringLiteral:
                        if (!awaitingCloseBracket && (current == '"' || current == '\''))
                        {
                            state = State.Normal;
                            tokenBuilder.Append(current);
                            tokens.Add(tokenBuilder.ToString());
                            tokenBuilder.Clear();
                        }
                        else if (awaitingCloseBracket && current == ']' && next == ']')
                        {
                            state = State.Normal;
                            tokenBuilder.Append("]]");
                            awaitingCloseBracket = false;
                            tokens.Add(tokenBuilder.ToString());
                            tokenBuilder.Clear();
                            i++;
                        }
                        else
                        {
                            tokenBuilder.Append(current);
                        }
                        break;
                }
            }

            if (tokenBuilder.Length > 0)
            {
                tokens.Add(tokenBuilder.ToString());
            }

            return tokens;
        }

        internal static string FormatIndentions(string unformattedString)
        {
            var tokens = Tokenize(unformattedString);
            int indentLevel = 0;
            StringBuilder formattedCode = new StringBuilder();

            for (int i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];
                if (token == "\n")
                {
                    if (i + 1 < tokens.Count && (tokens[i + 1] == "end" || tokens[i + 1] == "end," || tokens[i + 1] == "until" || tokens[i + 1] == "}" || tokens[i + 1] == "},"))
                    {
                        indentLevel--;
                    }

                    if (indentLevel < 0)
                    {

                        indentLevel = 0;
                    }
                    formattedCode.Append("\n" + new string('\t', indentLevel));
                }
                else
                {
                    formattedCode.Append(token + " ");
                }

                if (token == "function()" || token == "if" || token == "for" || token == "while" || token == "repeat" || token == "do" || token == "{")
                {
                    indentLevel++;
                }
            }
            return formattedCode.ToString();
        }

    }
}
