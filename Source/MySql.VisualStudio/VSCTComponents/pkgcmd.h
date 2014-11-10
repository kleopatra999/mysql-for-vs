// Copyright © 2014, Oracle and/or its affiliates. All rights reserved.
//
// MySQL for Visual Studio is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

//////////////////////////////////////////////////////////////////////////
//                                                                      //
//////////////////////////////////////////////////////////////////////////


// pkgcmd.h : Menu commands defined for this Environment package

// do not use #pragma once - it is used by ctc compiler
#ifndef __PKGCMD_H_
#define __PKGCMD_H_

#define guidMySqlProviderPkg                        {79a115c9-b133-4891-9e7b-242509dad272}
#define Group_Undefined                             {83285929-227c-11d3-b870-00c04f79f802}
#define Pkg_Undefined                               {8328592a-227c-11d3-b870-00c04f79f802}

#define guidVSData                                  {4614107f-217d-4bbf-9dfe-b9e165c65572}
#define IDG_DV_CONNECTION                           0x4101
#define IDG_DV_GLOBAL1                              0x4001
#define IDG_DV_OBJECT                               0x4301
#define IDG_DV_STATIC                               0x4401

#define guidMySqlProviderCmdSet                     {b87cb51f-8a01-4c5e-bf3e-5d0565d5397d}
#define cmdidAddNewFunctionGlobal                   0x01F7
#define cmdidAddNewProcedureGlobal                  0x01F6
#define cmdidAddNewTableGlobal                      0x01F4
#define cmdidAddNewUDFGlobal                        0x01F8
#define cmdidAddNewViewGlobal                       0x01F5
#define cmdidAlterProcedure                         0x0121
#define cmdidAlterTrigger                           0x0141
#define cmdidAlterView                              0x0111
#define cmdidCreateFunction                         0x0130
#define cmdidCreateProcedure                        0x0120
#define cmdidCreateTable                            0x0100
#define cmdidCreateTrigger                          0x0140
#define cmdidCreateUDF                              0x0150
#define cmdidCreateView                             0x0110
#define cmdidDelete                                 0x0151
#define groupAddNew2005                             0x03EA
#define groupAddNew2008                             0x03EB
#define menuAddNew2005                              0x03E8
#define menuAddNew2008                              0x03E9

#define guidOfficeIcon                              {d309f794-903f-11d0-9efc-00a0c911004f}
#define msotcidNoIcon                               0x02EA


#endif  // __PKGCMD_H_
