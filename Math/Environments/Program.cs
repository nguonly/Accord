// Accord.NET Sample Applications
// http://accord.googlecode.com
//
// Copyright © César Souza, 2009-2012
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math.Environments.Octave;
using Accord.Math;

namespace Environments
{
    class Program : OctaveEnvironment
    {
        static void Main(string[] args)
        {
            // Let I be the 5x5 identity matrix
            var I = eye(5);

            // [1 1 1 1 1] = sum(I)
            var s = sum(I);

            var p = s.Multiply(pi);

            // I * pi = pi
            p.IsEqual(pi); // true

        }
    }
}
