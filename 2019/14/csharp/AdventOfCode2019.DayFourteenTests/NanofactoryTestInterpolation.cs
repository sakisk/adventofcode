﻿using System.Collections.Generic;
using AdventOfCode2019.DayFourteen;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayFourteenTests
{
    public class NanofactoryTestInterpolation
    {
        [Theory]
        [MemberData(nameof(ReactionInputs))]
        public void NanofactoryTestFuelProductionEstimationForTrillionOre(string input, int fuelForTrilionOre)
        {
            var nanofactory = new Nanofactory(input);
            const long ore = 1000000000000L;
            var fuel = 1L;
            var newFuel = 0;
            
            while (true)
            {
                // analyzing 1000 first iterations of nanofactory produces fuel which increase with constant rate
                // y = l * x where y is ore used to produce x fuel
                // so when fuel increases ore increases by a constant factor l
                // which will follow the magnitude of current fuel produced towards 10^12
                newFuel = (int) ((double) ore / nanofactory.React(fuel) * fuel);

                if (newFuel == fuel)
                    break;

                fuel = newFuel;
            }

            newFuel.Should().Be(fuelForTrilionOre);
        }

        public static IEnumerable<object[]> ReactionInputs => new[]
        {
            new object[]
            {
                @"157 ORE => 5 NZVS
165 ORE => 6 DCFZ
44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
179 ORE => 7 PSHF
177 ORE => 5 HKGWZ
7 DCFZ, 7 PSHF => 2 XJWVT
165 ORE => 2 GPVTF
3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT",
                82892753
            },
            new object[]
            {
                @"2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG
17 NVRVD, 3 JNWZP => 8 VPVL
53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL
22 VJHF, 37 MNCFX => 5 FWMGM
139 ORE => 4 NVRVD
144 ORE => 7 JNWZP
5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC
5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV
145 ORE => 6 MNCFX
1 NVRVD => 8 CXFTF
1 VJHF, 6 MNCFX => 4 RFSQX
176 ORE => 6 VJHF",
                5586022
            },
            new object[]
            {
                @"171 ORE => 8 CNZTR
7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
114 ORE => 4 BHXH
14 VRPVC => 6 BMBT
6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
5 BMBT => 4 WPTQ
189 ORE => 9 KTJDG
1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
12 VRPVC, 27 CNZTR => 2 XDBXC
15 KTJDG, 12 BHXH => 5 XCVML
3 BHXH, 2 VRPVC => 7 MZWV
121 ORE => 7 VRPVC
7 XCVML => 6 RJRHP
5 BHXH, 4 VRPVC => 5 LTCX",
                460664
            },
            new object[]
            {
                @"4 DGXQJ => 5 QNMV
10 WHSGM => 6 LFXWM
3 XRJH, 15 FVRFC, 19 DGXQJ, 2 BZWFZ, 8 XDQG, 1 LFXWM, 6 CDRP => 1 LWKJL
1 TLGRN => 5 BDPJD
1 DBGK, 5 DTWF => 3 FVRFC
7 NMWGH => 9 CGRFH
1 TLRZ => 2 XRJH
1 RGRHS => 7 WSGW
1 DGXQJ, 15 PWXFD, 9 XRJH => 4 LGVS
5 QHGP, 1 WHSGM => 7 DBGK
1 RHKVX => 5 CDRP
6 VMVJS => 5 VFVBP
1 WSGW => 6 PGBK
1 FXLD, 1 VMVJS => 8 PGJC
4 WCWLK => 1 KCHWM
11 XDQG => 2 QMVLD
137 ORE => 4 KRSK
4 KRSK => 5 HSCF
1 KRSK => 7 XPGP
4 BZWFZ, 1 TLGRN, 6 CTBV => 1 CPXLQ
9 WNVTR, 3 FVRFC, 6 CTBV => 2 RGRHS
5 KRSK => 3 JLSHT
4 DHJD => 2 DTWF
9 PGJC => 9 RNJCV
1 KCHWM, 10 DGXQJ => 4 PWXFD
6 KSJPW => 8 DHJD
6 DBGK, 1 ZPVDZ => 3 BJLQG
1 WNVTR, 2 XRJH => 9 ZPVDZ
3 DHJD => 8 KVKM
2 HSCF, 1 TLRZ => 9 QHGP
1 PLDS, 7 BJLQG, 1 WNVTR => 3 XDQG
14 CTBV, 23 PLDS, 5 MCNR => 9 TLGRN
1 PWXFD => 7 DJSW
2 DJSW => 6 MCNR
1 CTBV, 7 PGBK, 5 BDPJD, 5 DTWF, 12 PLDS, 31 RNJCV, 2 KVKM => 6 DJLXD
2 XRJH, 2 FXLD => 2 WHSGM
4 XPGP, 12 PWXFD => 9 FXLD
2 LGVS, 1 VMVJS, 1 QNMV => 3 HKXCV
1 WCWLK => 6 TLRZ
115 ORE => 7 TKMGN
2 TLGRN, 2 DHJD, 1 MCNR => 9 SCZCQ
13 FVRFC => 5 XBLQD
5 XDQG, 1 DZJLT => 4 CPGS
5 XPGP, 1 DHJD => 6 CTBV
1 XRJH, 2 KCHWM, 1 FXLD => 9 FSRD
5 CTBV, 1 CDRP, 5 RNJCV => 6 DZJLT
151 ORE => 6 WCWLK
4 HKXCV => 9 PLDS
16 KCHWM => 6 VKPGK
3 VKPGK => 1 HCMT
5 QMVLD, 8 HCMT, 25 CPXLQ, 29 JLSHT, 9 CPGS, 8 RHKVX, 19 DQTLW, 5 LWKJL, 2 DJLXD, 14 CVBQ, 7 SCZCQ, 17 FSRD, 3 JLHWQ, 6 XDQG => 1 FUEL
1 FXLD, 1 VMVJS => 8 NMWGH
2 KCHWM, 6 HSCF => 9 KSJPW
2 DBGK, 17 NMWGH => 1 JLHWQ
26 CDRP => 8 DQTLW
2 PLDS => 7 HPCR
6 LGVS, 21 DGXQJ => 4 RHKVX
6 VFVBP => 2 BZWFZ
2 XDQG, 3 BDPJD, 10 DJSW, 1 CGRFH, 3 HPCR, 2 RHKVX, 5 BZWFZ, 13 XBLQD => 7 CVBQ
8 TKMGN => 7 DGXQJ
1 JLSHT, 20 KSJPW => 7 VMVJS
16 DJSW, 1 PGJC, 4 FXLD => 8 WNVTR",
                8193614
            },
        };
    }
}