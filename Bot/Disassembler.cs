using System;
using Gee.External.Capstone;
using Gee.External.Capstone.Arm64;
using Gee.External.Capstone.Arm;
using Gee.External.Capstone.X86;
using Gee.External.Capstone.Mips;
using Gee.External.Capstone.PowerPc;
using Gee.External.Capstone.M68K;
using Gee.External.Capstone.XCore;
using Iced.Intel;
using System.Linq;
using System.Collections.Generic;

namespace paracordbot.Bot.Disassmbler
{
    class DisassemblerEx
    {
        public Arm64Instruction[] DisasmArm64(byte[] bytes)
        {
            const Arm64DisassembleMode disassembleMode = Arm64DisassembleMode.Arm;
            using (CapstoneArm64Disassembler disassembler = CapstoneDisassembler.CreateArm64Disassembler(disassembleMode))
            {
                disassembler.EnableInstructionDetails = true;
                disassembler.DisassembleSyntax = DisassembleSyntax.Intel;
                Arm64Instruction[] instructions = disassembler.Disassemble(bytes);
                return instructions;
            }
        }

        public X86Instruction[] Disasmx86(byte[] bytes)
        {
            const X86DisassembleMode _x86DisassembleMode = X86DisassembleMode.Bit32;

            using (CapstoneX86Disassembler disassembler = CapstoneX86Disassembler.CreateX86Disassembler(_x86DisassembleMode))
            {
                disassembler.EnableInstructionDetails = true;
                disassembler.DisassembleSyntax = DisassembleSyntax.Intel;
                X86Instruction[] instructions = disassembler.Disassemble(bytes);
                return instructions;
            }
        }

        public MipsInstruction[] Disasmips32(byte[] bytes)
        {

            const MipsDisassembleMode mipsmode = MipsDisassembleMode.Bit32;
            using (CapstoneMipsDisassembler disassembler = CapstoneX86Disassembler.CreateMipsDisassembler(mipsmode))
            {
                disassembler.EnableInstructionDetails = true;
                disassembler.DisassembleSyntax = DisassembleSyntax.Intel;
                MipsInstruction[] instructions = disassembler.Disassemble(bytes);
                return instructions;
            }
        }

        public MipsInstruction[] Disasmips64(byte[] bytes)
        {

            const MipsDisassembleMode mipsmode = MipsDisassembleMode.Bit64;
            using (CapstoneMipsDisassembler disassembler = CapstoneX86Disassembler.CreateMipsDisassembler(mipsmode))
            {
                disassembler.EnableInstructionDetails = true;
                disassembler.DisassembleSyntax = DisassembleSyntax.Intel;
                MipsInstruction[] instructions = disassembler.Disassemble(bytes);
                return instructions;
            }
        }


        public X86Instruction[] Disasmx64(byte[] bytes)
        {
            const X86DisassembleMode _x86DisassembleMode = X86DisassembleMode.Bit64;

            using (CapstoneX86Disassembler disassembler = CapstoneX86Disassembler.CreateX86Disassembler(_x86DisassembleMode))
            {
                disassembler.EnableInstructionDetails = true;
                disassembler.DisassembleSyntax = DisassembleSyntax.Intel;
                X86Instruction[] instructions = disassembler.Disassemble(bytes);
                return instructions;
            }
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}

