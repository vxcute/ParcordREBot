using System;
using Gee.External.Capstone;
using Gee.External.Capstone.Arm64;
using Gee.External.Capstone.X86;
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
    }
}



