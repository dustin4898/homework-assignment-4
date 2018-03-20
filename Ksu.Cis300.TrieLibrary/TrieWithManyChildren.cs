﻿/* Trie.cs
 * Author: Rod Howell
 */
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.TrieLibrary
{
    /// <summary>
    /// A single node of a trie with more than one child.
    /// </summary>
    public class TrieWithManyChildren : ITrie
    {
        /// <summary>
        /// Indicates whether the trie rooted at this node contains the empty string.
        /// </summary>
        private bool _hasEmptyString = false;

        /// <summary>
        /// The children of this node.
        /// </summary>
        private ITrie[] _children = new ITrie[26];

        /// <summary>
        /// Constructs a trie containing the given string and having the given child at the given label.
        /// If s contains any characters other than lower-case English letters,
        /// throws an ArgumentException.
        /// If childLabel is not a lower-case English letter, throws an ArgumentException.
        /// </summary>
        /// <param name="s">The string to include.</param>
        /// <param name="hasEmpty">Indicates whether this trie should contain the empty string.</param>
        /// <param name="childLabel">The label of the child.</param>
        /// <param name="child">The child labeled childLabel.</param>
        public TrieWithManyChildren(string s, bool hasEmpty, char childLabel, ITrie child)
        {
            if (childLabel < 'a' | childLabel > 'z')
            {
                throw new ArgumentException();
            }
            _hasEmptyString = hasEmpty;
            _children[childLabel - 'a'] = child;
            Add(s);
        }

        /// <summary>
        /// Determines whether the trie rooted at this node contains the given string.
        /// </summary>
        /// <param name="s">The string to look up.</param>
        /// <returns>Whether the trie contains s.</returns>
        public bool Contains(string s)
        {
            if (s == "")
            {
                return _hasEmptyString;
            }
            else if (s[0] < 'a' || s[0] > 'z')
            {
                return false;
            }
            else
            {
                int loc = s[0] - 'a';
                if (_children[loc] == null)
                {
                    return false;
                }
                else
                {
                    return _children[loc].Contains(s.Substring(1));
                }
            }
        }

        /// <summary>
        /// Adds the given string to the trie rooted at this node.
        /// </summary>
        /// <param name="s">The string to add.</param>
        /// <returns>The resulting trie.</returns>
        public ITrie Add(string s)
        {
            if (s == "")
            {
                _hasEmptyString = true;
            }
            else if (s[0] < 'a' || s[0] > 'z')
            {
                throw new ArgumentException();
            }
            else
            {
                int loc = s[0] - 'a';
                if (_children[loc] == null)
                {
                    _children[loc] = new TrieWithNoChildren();
                }
                _children[loc] = _children[loc].Add(s.Substring(1));
            }
            return this;
        }

        /// <summary>
        /// Gets all of the strings that form words in this trie when appended to the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix</param>
        /// <returns>A trie containing all of the strings that form words in this trie when appended
        /// to the given prefix.</returns>
        public ITrie GetCompletions(string prefix)
        {
            if (prefix == "")
            {
                return this;
            }
            else if (prefix[0] < 'a' || prefix[0] > 'z')
            {
                return null;
            }
            else
            {
                int loc = prefix[0] - 'a';
                if (_children[loc] == null)
                {
                    return null;
                }
                else
                {
                    return _children[loc].GetCompletions(prefix.Substring(1));
                }
            }
        }

        /// <summary>
        /// Adds all of the strings in this trie alphabetically to the end of the given list, with each
        /// string prefixed by the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="list">The list to which the strings are to be added.</param>
        public void AddAll(StringBuilder prefix, IList list)
        {
            if (_hasEmptyString)
            {
                list.Add(prefix.ToString());
            }
            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] != null)
                {
                    prefix.Append((char)(i + 'a'));
                    _children[i].AddAll(prefix, list);
                    prefix.Length--;
                }
            }
        }
    }
}
