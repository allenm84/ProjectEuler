﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Data;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem122 : EulerProblem
  {
    #region A003313
    const string Table = @"1 0
2 1
3 2
4 2
5 3
6 3
7 4
8 3
9 4
10 4
11 5
12 4
13 5
14 5
15 5
16 4
17 5
18 5
19 6
20 5
21 6
22 6
23 6
24 5
25 6
26 6
27 6
28 6
29 7
30 6
31 7
32 5
33 6
34 6
35 7
36 6
37 7
38 7
39 7
40 6
41 7
42 7
43 7
44 7
45 7
46 7
47 8
48 6
49 7
50 7
51 7
52 7
53 8
54 7
55 8
56 7
57 8
58 8
59 8
60 7
61 8
62 8
63 8
64 6
65 7
66 7
67 8
68 7
69 8
70 8
71 9
72 7
73 8
74 8
75 8
76 8
77 8
78 8
79 9
80 7
81 8
82 8
83 8
84 8
85 8
86 8
87 9
88 8
89 9
90 8
91 9
92 8
93 9
94 9
95 9
96 7
97 8
98 8
99 8
100 8
101 9
102 8
103 9
104 8
105 9
106 9
107 9
108 8
109 9
110 9
111 9
112 8
113 9
114 9
115 9
116 9
117 9
118 9
119 9
120 8
121 9
122 9
123 9
124 9
125 9
126 9
127 10
128 7
129 8
130 8
131 9
132 8
133 9
134 9
135 9
136 8
137 9
138 9
139 10
140 9
141 10
142 10
143 10
144 8
145 9
146 9
147 9
148 9
149 9
150 9
151 10
152 9
153 9
154 9
155 10
156 9
157 10
158 10
159 10
160 8
161 9
162 9
163 9
164 9
165 9
166 9
167 10
168 9
169 10
170 9
171 10
172 9
173 10
174 10
175 10
176 9
177 10
178 10
179 10
180 9
181 10
182 10
183 10
184 9
185 10
186 10
187 10
188 10
189 10
190 10
191 11
192 8
193 9
194 9
195 9
196 9
197 10
198 9
199 10
200 9";
    #endregion

    public override int Number { get { return 122; } }

    public override object Solve()
    {
      var aN = Table
        .SplitBy('\r', '\n')
        .Select(l => l
          .SplitBy(' ')
          .Select(p => int.Parse(p.Trim()))
          .ToArray())
        .ToDictionary(k => k[0], v => v[1]);

      int sum = 0;
      for (int k = 1; k <= 200; ++k)
      {
        sum += aN[k];
      }
      return sum;
    }
  }
}
