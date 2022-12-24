# ChASM-x86-x64-Assembly-Syntax-Highlighter
 Low budget syntax highlighter for Win64 Asm dudes (quick and dirty solution).

This is temporary solution which was created because [Henk-JanLebbink.AsmDude](https://marketplace.visualstudio.com/items?itemName=Henk-JanLebbink.AsmDude) does not work under Visual Studio 2022 when writing this.

The ethical.blue Magazine has good news for all win64 asm dudes. While waiting for new version of Henk-JanLebbink.AsmDude Visual Studio extension I have decided to create in two business days a simple and free MASM x64 syntax highlighter (named ChASM).

ChASM is x86/x64 Assembly Language (MASM) syntax highlighting extension for Visual Studio. The general idea is to provide ANY syntax highlighting without costs. 💵 Notice that this quick and dirty solution is based on simple parser and hacks rather than lexical analysis, tokenization etc.

# The Problem: No Syntax Highlighting for x86/x64 Assembly

Microsoft Visual Studio Community 2022 Version 17.3.5 does not provide any syntax highlighting for code in x86/x64 Assembly Language (MASM).

![image](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/problem.png)

# The Second Problem: AsmDude Extension Does Not Work

This is temporary solution which was created because [Henk-JanLebbink.AsmDude](https://marketplace.visualstudio.com/items?itemName=Henk-JanLebbink.AsmDude) does not work under Visual Studio 2022 when writing this.

![image](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/asm-dude-not-working1.png)

# ChASM is x86/x64 Assembly Syntax Colorizer

The idea of ChASM extension is to provide basic syntax coloring for x86/x64 Assembly Language (MASM) in Microsoft Visual Studio.

![image](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/broken.png)

# ChASM Features

💥 Highlight General Purpose Registers (x86/x64)
💥 Descriptions for Over 1000 Mnemonics
💥 150+ General Purpose Registers with Description
💥 Detect Common WinAPI Functions Calls
💥 Syntax Help for Over 1500 Mostly Used WinAPI Functions
💥 Colorize One Line Comments
💥 Highlight System Calls (SYSCALL)
💥 Signature Help for Mnemonics

![ChASM by Dawid Farbaniec](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/001.png)

![ChASM by Dawid Farbaniec](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/002.png)

![ChASM by Dawid Farbaniec](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/003.png)

![ChASM by Dawid Farbaniec](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/004.png)

![ChASM by Dawid Farbaniec](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/005.png)

![ChASM by Dawid Farbaniec](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/006.png)

![ChASM by Dawid Farbaniec](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/007.png)

![ChASM by Dawid Farbaniec](https://github.com/ethicalblue/ChASM-x86-x64-Assembly-Syntax-Highlighter/blob/main/images/008.png)


# Download ChASM from Visual Studio Marketplace

https://marketplace.visualstudio.com/items?itemName=ethicalblue.chasm

# Credits

https://learn.microsoft.com/en-us/visualstudio/extensibility/creating-an-extension-with-an-editor-item-template?view=vs-2022

https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-highlighting-text?view=vs-2022&tabs=csharp

https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-creating-a-margin-glyph?view=vs-2022&tabs=csharp

https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-linking-a-content-type-to-a-file-name-extension?view=vs-2022

https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-statement-completion?view=vs-2022&tabs=csharp

[Advanced Micro Devices Inc., 2017 – AMD64 Architecture Programmer's Manual](https://www.amd.com/)

[Intel Corporation, 2019 – Intel 64 and IA-32 Architectures Software Developer's Manual](https://intel.com)

https://github.com/HJLebbink/asm-dude

https://marketplace.visualstudio.com/items?itemName=Henk-JanLebbink.AsmDude

https://www.felixcloutier.com/x86/