# Re:Mind
Re:Mind is a multi language transcompiler to multi target language



The Japanese transcompiler language Re:Mind is an open implementation language specification, and 
anyone can implement a compiler/transcompiler.

We do not reject the implementation as a compiler for the original runtime code, existing 
intermediate code, etc., but in view of the diversification of today's system development 
languages, interoperability with those language environments is facilitated and progressive 
introduction In order to achieve this, it is recommended to implement it as a transcompiler 
that targets other programming languages.

The Japanese transcompiler language Re:Mind, like the respected first-generation Japanese 
programming language Mind and later Japanese programming languages, adopts a Japanese-based 
syntax and uses English programming. While sharing the point that special characters such as 
``;'' and ``{ }'' that are common in languages are not used as delimiters, we allow the 
syntax of the target language in half-angle brackets and some syntax.

In addition, the Japanese transcompiler language Re:Mind uses double-byte symbols such as ◇, 〇,
 ·, □ as the start symbol of the control syntax, and uses the Japanese logic description language
  Re:Mind to create itemized Japanese sentences. It also shares Mind's syntax. ◇ indicates the 
  branch syntax, and 〇 indicates the start and end of the loop syntax. We have taken into 
  consideration that those familiar with flow diagram expressions can intuitively recognize 
  even without redundant Japanese notation.
