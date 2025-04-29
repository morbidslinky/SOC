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
            bool awaitingCloseDoubleBracket = false;
            bool awaitingCloseSingleBracket = false;

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
                            awaitingCloseDoubleBracket = true;
                            tokenBuilder.Append("[[");
                            i++;
                        }
                        else if (current == '"' || current == '\'')
                        {
                            state = State.StringLiteral;
                            awaitingCloseDoubleBracket = false;
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
                        else if (current == ';')
                        {
                            if (tokenBuilder.Length > 0)
                            {
                                tokens.Add(tokenBuilder.ToString());
                                tokenBuilder.Clear();
                            }
                            tokens.Add("\n");
                        }
                        else if (current == '=' && next != '=' && luaCode[i - 1] != '=' && luaCode[i - 1] != '~'  && luaCode[i - 1] != '>' && luaCode[i - 1] != '<')
                        {
                            if (tokenBuilder.Length > 0)
                            {
                                tokens.Add(tokenBuilder.ToString());
                                tokenBuilder.Clear();
                            }

                            tokenBuilder.Append(current);
                            tokens.Add(tokenBuilder.ToString());
                            tokenBuilder.Clear();
                        }
                        else if (current == ',' || current == '{' || current == '}' || current == '(' || current == ')')
                        {
                            if (tokenBuilder.Length > 0)
                            {
                                tokens.Add(tokenBuilder.ToString());
                                tokenBuilder.Clear();
                            }

                            tokenBuilder.Append(current);
                            tokens.Add(tokenBuilder.ToString());
                            tokenBuilder.Clear();
                        }
                        else
                        {
                            if (current == '[')
                            {
                                awaitingCloseSingleBracket = true;
                            }
                            else if (current == ']' && awaitingCloseSingleBracket)
                            {
                                awaitingCloseSingleBracket = false;
                            }
                            tokenBuilder.Append(current);
                        }
                        break;

                    case State.SingleLineComment:
                        var prev = luaCode[i - 1];
                        var prevPrev = luaCode[i - 2];
                        if (prevPrev == '-' && prev == '-' && current == '[' && next == '[')
                        {
                            state = State.MultiLineComment;
                            awaitingCloseDoubleBracket = true;
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
                        if (current == ']' && next == ']' && awaitingCloseDoubleBracket)
                        {
                            state = State.Normal;
                            awaitingCloseDoubleBracket = false;
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

                        if (!awaitingCloseDoubleBracket && (current == '"' || current == '\''))
                        {
                            state = State.Normal;
                            tokenBuilder.Append(current);
                            if (awaitingCloseSingleBracket && next == ']')
                            {
                                tokenBuilder.Append(next);
                                awaitingCloseSingleBracket = false;
                                i++;
                            }
                            tokens.Add(tokenBuilder.ToString());
                            tokenBuilder.Clear();
                        }
                        else if (awaitingCloseDoubleBracket && current == ']' && next == ']')
                        {
                            state = State.Normal;
                            tokenBuilder.Append("]]");
                            awaitingCloseDoubleBracket = false;
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
                string nextToken = i + 1 < tokens.Count ? tokens[i + 1] : null;
                string prevToken = i - 1 >= 0 ? tokens[i - 1] : null;
                string prevPrevToken = i - 2 >= 0 ? tokens[i - 2] : null;

                if (token == "\n")
                {
                    if (nextToken != null && (nextToken == "end" || nextToken == "until" || nextToken == "}" || nextToken == "else" || nextToken == "elseif"))
                    {
                        indentLevel--;
                    }
                    else if (prevToken != null && (prevToken == "end" || prevToken == "until" || prevToken == "}") && prevPrevToken != "\n")
                    {
                        indentLevel--;
                    }

                    if (indentLevel < 0)
                    {
                        indentLevel = 0;
                    }
                    formattedCode.Append('\n');
                    formattedCode.Append(new string('\t', indentLevel));
                }
                else if ((token == "end" || token == "until" || token == "}" || token == "else" || token == "elseif") &&
                         prevToken != "\n" &&
                         nextToken != "\n")
                {
                    formattedCode.Append(token);
                    formattedCode.Append(" ");
                    indentLevel--;
                }
                else
                {
                    formattedCode.Append(token);
                    if ((token != "(" && nextToken != "," && nextToken != ")" && nextToken != "(") && !(token.EndsWith("]") && nextToken.StartsWith("[")))
                    {
                        formattedCode.Append(" ");
                    }
                }

                if (token == "function" || token == "then" || token == "repeat" || token == "do" || token == "{" || token == "else")
                {
                    indentLevel++;
                }
            }
            return formattedCode.ToString();
        }

    }
}
