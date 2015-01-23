using System;
using System.Collections.Generic;

namespace Harness.Support {

    public class ErrorMessage {
        private readonly IList<string> messageParts = new List<string>();

        public static ErrorMessage Type(Type type) {
            return new ErrorMessage {
                messageParts =
                {
                    {"Type: " + type.FullName},
                }
            };
        }

        public ErrorMessage AddBlock(params string[] lines) {
            messageParts.Add("\n");
            foreach (var line in lines) {
                messageParts.Add(line);
            }
            messageParts.Add("\n");
            return this;
        }

        public ErrorMessage Cannot(string action) {
            messageParts.Add("Cannot: " + action + ".");
            return this;
        }

        public ErrorMessage ToType(Type type) {
            messageParts.Add("To Type: " + type.FullName);
            return this;
        }

        public static implicit operator string(ErrorMessage error) {
            return string.Join(" ", error.messageParts);
        }

        public override string ToString() {
            return (string)this;
        }
    }
}