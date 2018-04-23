//#define MAP_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class mapController : MonoBehaviour {

	//public
	//public map parts prefab
	public GameObject mapPartsPrefab;
	//public side map parts prefab
	public GameObject sideMapPartsPrefab;
	//public back star prefab
	public GameObject backStarPrefab;

	//map stage
	public int ms50 = 0x00;	//map50 for title
	public int ms100 = 0x01;	//map100 for stage1
	public int ms200 = 0x02;	//map200 for stage2
	public int ms300 = 0x03;	//map300 for stage3
	public int ms350 = 0x04;	//map350 for stage3-2
	public int ms360 = 0x05;	//map360 for stage3-3
	public int msdummy = 0x06;	//map dummy

	//side map stage
	public int sms50 = 0x00;	//side map 50 for title
	public int sms100 = 0x01;	//side map100 for stage1 (none)
	public int sms200 = 0x02;	//side map200 for stage2 (none)
	public int sms300 = 0x03;	//side map300 for stage3 (none)
	public int sms350 = 0x04;	//side map350 for stage3-2 (none)
	public int sms360 = 0x05;	//side map360 for stage3-3 (none)
	public int smsdummy = 0x06;	//side map dummy

	//private
	//const
	//map data
	string[] mapdata = new string[]{
		//map50 for title
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"38 39 38 39 38 39 38 39 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 27 28 27 28 27 28 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 27 1C 1C 28 27 28 " + 
		"61 62 61 62 61 62 61 62 " + 
		"27 28 27 1C 1C 28 27 28 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 42 43 44 45 27 28 " + 
		"42 43 FF FF FF FF 44 45 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF 1D 1E 1F 20 FF FF " + 
		"FF FF 6D 2A 2B 6E FF FF " + 
		"FF FF 1A FF FF 1B FF FF " + 
		"1D 1E 6D 20 1D 6E 1F 20 " + 
		"1A 1B 1A 1B 1A 1B 1A 1B " + 
		"6D 6E 6D 1C 1C 6E 6D 6E " + 
		"1A 1B 1A 1C 1C 1B 1A 1B " + 
		"2A 2B 1A 6E 6D 1B 2C 2D " + 
		"FF FF 2A 1B 1A 2B FF FF " + 
		"FF FF FF 2A 2B FF FF FF " +
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " 
		,
		//map100 for stage1
		"03 03 03 03 04 03 04 03 " +
		"03 03 03 03 03 03 03 03 " +
		"03 03 03 04 03 03 04 03 " +
		"04 04 04 04 04 03 04 04 " +
		"0B 0B 0B 0B 0B 0B 0B 0B " +
		"0C 0C 0C 0C 0B 0B 0B 0C " +
		"05 05 10 0C 0C 0B 0C 12 " +
		"07 07 09 13 0C 0B 0C 0F " +
		"08 08 02 01 13 0C 12 01 " +
		"06 06 10 00 07 12 07 06 " +
		"02 02 09 05 01 02 0D 08 " +
		"07 07 06 07 0E 07 05 13 " +
		"02 01 02 10 0B 0F 02 01 " +
		"07 06 07 06 05 05 07 06 " +
		"01 02 01 02 01 09 0F 02 " +
		"06 07 06 08 06 07 06 07 " +
		"01 02 01 02 01 02 05 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 00 01 02 01 " +
		"07 06 07 06 09 08 08 06 " +
		"01 02 01 02 01 11 05 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"08 08 01 02 01 02 01 02 " +
		"07 07 05 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 08 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"01 01 0A 0D 02 01 02 01 " +
		"0E 0E 0B 0C 00 0E 09 0E " +
		"03 03 04 03 00 11 10 03 " +
		"03 03 03 03 04 0F 10 03 " +
		"04 04 03 04 0C 0F 09 0B " +
		"05 05 12 12 0C 0C 10 0C " +
		"02 02 01 09 0C 0C 0F 11 " +
		"06 07 06 07 05 12 05 07 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 06 07 " +
		"02 01 02 01 02 01 02 01 " +
		"07 06 07 06 07 06 07 06 " +
		"01 02 01 02 01 02 01 02 " +
		"06 07 06 07 06 07 05 07 " +
		"02 01 02 01 02 01 02 01 " +
		"08 08 08 06 07 06 07 06 " +
		"11 11 05 02 01 02 01 02 " +
		"06 07 06 07 06 08 06 07 " +
		"01 02 01 0E 01 02 01 02 " +
		"06 07 10 0C 0F 07 08 00 " +
		"08 08 02 05 02 01 05 01 " +
		"0C 0C 00 0D 0E 00 07 06 " +
		"0C 0C 0E 0C 0C 0E 0E 0E " +
		"0B 0B 0C 0B 0B 0C 00 12 " +
		"04 04 0B 04 04 0B 0F 08 " +
		"04 03 04 03 04 0B 0C 0C " +
		"03 03 03 03 03 04 0B 0B " +
		"03 03 03 03 03 03 04 04 " +
		"04 03 03 03 03 03 03 03 " +
		"04 03 04 03 04 03 03 03 " +
		"03 03 03 04 03 03 03 03 " +
		"03 03 03 04 03 03 03 03 " +
		"04 03 03 03 03 03 03 03 " +
		"03 04 03 03 04 03 04 03 " +
		"03 03 03 03 03 03 03 03 " +
		"04 03 03 03 04 03 03 03 " +
		"04 03 03 03 04 03 03 03 " +
		"03 03 04 03 03 03 04 04 " +
		"03 03 03 03 03 03 03 03 " +
		"03 03 03 03 03 03 03 03 " +
		"03 03 03 03 03 03 03 03 " +
		"03 03 03 03 03 03 03 03 " +
		"03 03 03 03 04 03 03 03 " +
		"03 04 03 03 04 03 03 03 " +
		"04 03 04 04 03 03 03 03 " +
		"03 03 03 03 03 03 03 03 " +
		"03 03 03 03 03 04 03 03 " +
		"03 03 03 03 03 03 03 03 "
		,
		//map200 for stage2
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"65 66 65 66 65 66 65 66 " + 
		"71 72 71 72 71 72 71 72 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"50 51 50 51 50 51 50 51 " + 
		"5C 5D 5C 5D 5C 5D 5C 5D " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"38 39 38 39 38 39 38 39 " + 
		"44 45 44 45 44 45 44 45 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"1F 20 1F 20 1F 20 1F 20 " + 
		"2C 2D 2C 2D 2C 2D 2C 2D " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"38 39 38 39 38 39 38 39 " + 
		"44 45 44 45 44 45 44 45 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"50 51 50 51 50 51 50 51 " + 
		"5C 5D 5C 5D 5C 5D 5C 5D " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"65 66 65 66 65 66 65 66 " + 
		"71 72 71 72 71 72 71 72 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"38 39 38 39 38 39 38 39 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 27 28 27 28 27 28 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 27 1C 1C 28 27 28 " + 
		"61 62 61 62 61 62 61 62 " + 
		"27 28 27 1C 1C 28 27 28 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 42 43 44 45 27 28 " + 
		"42 43 FF FF FF FF 44 45 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF 1F 20 1F 20 FF FF " + 
		"FF FF 6D 2D 2C 6E FF FF " + 
		"FF FF 1A FF FF 1B FF FF " + 
		"1D 1E 6D 1E 1D 6E 1D 1E " + 
		"1A 1B 1A 1B 1A 1B 1A 1B " + 
		"6D 6E 6D 1C 1C 6E 6D 6E " + 
		"1A 1B 1A 1C 1C 1B 1A 1B " + 
		"2A 2B 6D 6E 6D 6E 2C 2D " + 
		"34 33 2A 1B 1A 2B 34 33 " + 
		"59 58 59 2A 2B 58 59 58 " + 
		"34 33 34 33 34 33 34 33 " + 
		"59 58 59 58 59 58 59 58 " + 
		"34 33 34 33 34 33 34 33 " + 
		"5D 5C 5D 5C 5D 5C 5D 5C " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF "
		,
		//map300 for stage3
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"17 18 19 FF FF 14 15 16 " + 
		"24 25 26 FF FF 21 22 23 " + 
		"FF 31 32 FF FF 2E 2F FF " + 
		"FF 3D 3E FF FF 3A 3B FF " + 
		"FF 31 32 19 14 2E 2F FF " + 
		"FF 3D 3E 16 17 3A 3B FF " + 
		"49 4A 4B 57 52 46 47 48 " + 
		"55 56 57 FF FF 52 53 54 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"16 17 15 16 17 18 16 17 " + 
		"23 24 22 23 24 25 23 24 " + 
		"48 49 47 48 49 4A 48 49 " + 
		"54 55 53 54 55 56 54 55 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " +
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " 
		,
		//map350 for stage3-2
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"5F 60 FF FF FF FF 5E 5F " + 
		"68 69 FF FF FF FF 67 68 " + 
		"6B 6C FF FF FF FF 6A 6B " +
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"5F 60 FF FF FF FF 5E 5F " + 
		"68 69 FF FF FF FF 67 68 " + 
		"6B 6C FF FF FF FF 6A 6B " +
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"5F 60 FF FF FF FF 5E 5F " + 
		"68 69 FF FF FF FF 67 68 " + 
		"68 69 FF FF FF FF 67 68 " + 
		"6B 6C FF FF FF FF 6A 6B " +
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"5F 60 FF FF FF FF 5E 5F " + 
		"68 69 FF FF FF FF 67 68 " + 
		"68 69 FF FF FF FF 67 68 " + 
		"68 69 FF FF FF FF 67 68 " + 
		"6B 6C FF FF FF FF 6A 6B " +
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"5F 60 FF FF FF FF 5E 5F " + 
		"68 69 FF FF FF FF 67 68 " + 
		"68 69 FF FF FF FF 67 68 " + 
		"68 69 FF FF FF FF 67 68 " + 
		"6B 6C FF FF FF FF 6A 6B " +
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"5F 60 FF FF FF FF 5E 5F " + 
		"68 69 FF FF FF FF 67 68 " + 
		"68 69 FF FF FF FF 67 68 " + 
		"6B 6C FF FF FF FF 6A 6B " +
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"5F 60 FF FF FF FF 5E 5F " + 
		"68 69 FF FF FF FF 67 68 " + 
		"6B 6C FF FF FF FF 6A 6B " +
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"5F 60 FF FF FF FF 5E 5F " + 
		"68 69 FF FF FF FF 67 68 " + 
		"6B 6C FF FF FF FF 6A 6B " +
		"FF FF 5E 5F 5F 60 FF FF " + 
		"FF FF 67 68 68 69 FF FF " + 
		"FF FF 6A 6B 6B 6C FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " 
		,
		//map360 for stage3-3
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " +
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " +
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " 
		,
		//mapdummy
		"03 03 03 03 04 03 04 03 " +
		"03 03 03 03 04 03 04 03 " +
		"03 03 03 03 04 03 04 03 " +
		"03 03 03 03 03 03 03 03 " +
		"03 03 03 04 03 03 04 03 " +
		"03 04 04 04 04 FF FF 04 " +
		"03 0B 0B 0B 0B FF 0B 0B " +
		"03 0C 0C 0C 0B FF FF 0C " +
		"03 05 10 0C 0C 0B 0C 12 " +
		"03 07 09 13 0C 0B 0C 0F " +
		"03 08 02 01 13 0C 12 01 " +
		"03 06 10 00 18 12 07 06 " +
		"03 02 09 18 01 02 0D 08 " +
		"03 07 06 07 0E 07 05 13 "
		,
		#if MAP_EDITOR
		//map for editor
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"65 66 65 66 65 66 65 66 " + 
		"71 72 71 72 71 72 71 72 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"50 51 50 51 50 51 50 51 " + 
		"5C 5D 5C 5D 5C 5D 5C 5D " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"38 39 38 39 38 39 38 39 " + 
		"44 45 44 45 44 45 44 45 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"1F 20 1F 20 1F 20 1F 20 " + 
		"2C 2D 2C 2D 2C 2D 2C 2D " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"38 39 38 39 38 39 38 39 " + 
		"44 45 44 45 44 45 44 45 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"50 51 50 51 50 51 50 51 " + 
		"5C 5D 5C 5D 5C 5D 5C 5D " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"65 66 65 66 65 66 65 66 " + 
		"71 72 71 72 71 72 71 72 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"38 39 38 39 38 39 38 39 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 27 28 27 28 27 28 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 27 1C 1C 28 27 28 " + 
		"61 62 61 62 61 62 61 62 " + 
		"27 28 27 1C 1C 28 27 28 " + 
		"61 62 61 1C 1C 62 61 62 " + 
		"27 28 42 43 44 45 27 28 " + 
		"42 43 FF FF FF FF 44 45 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF 1F 20 1F 20 FF FF " + 
		"FF FF 6D 2D 2C 6E FF FF " + 
		"FF FF 1A FF FF 1B FF FF " + 
		"1D 1E 6D 1E 1D 6E 1D 1E " + 
		"1A 1B 1A 1B 1A 1B 1A 1B " + 
		"6D 6E 6D 1C 1C 6E 6D 6E " + 
		"1A 1B 1A 1C 1C 1B 1A 1B " + 
		"2A 2B 6D 6E 6D 6E 2C 2D " + 
		"34 33 2A 1B 1A 2B 34 33 " + 
		"59 58 59 2A 2B 58 59 58 " + 
		"34 33 34 33 34 33 34 33 " + 
		"59 58 59 58 59 58 59 58 " + 
		"34 33 34 33 34 33 34 33 " + 
		"5D 5C 5D 5C 5D 5C 5D 5C " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF "
		#endif
	};

	//side map data
	string[] smapdata = new string[]{
		//side map50 for title
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"17 18 19 FF FF 14 15 16 " + 
		"24 25 26 FF FF 21 22 23 " + 
		"FF 31 32 FF FF 2E 2F FF " + 
		"FF 3D 3E FF FF 3A 3B FF " + 
		"FF 31 32 19 14 2E 2F FF " + 
		"FF 3D 3E 16 17 3A 3B FF " + 
		"49 4A 4B 57 52 46 47 48 " + 
		"55 56 57 FF FF 52 53 54 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"16 17 15 16 17 18 16 17 " + 
		"23 24 22 23 24 25 23 24 " + 
		"48 49 47 48 49 4A 48 49 " + 
		"54 55 53 54 55 56 54 55 " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " +
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " + 
		"FF FF FF FF FF FF FF FF " 
		,
		//side map100 for stage1
		"FF FF FF FF FF FF FF FF "	//dummy
		,
		//side map200 for stage2
		"FF FF FF FF FF FF FF FF "	//dummy
		,
		//side map300 for stage3
		"FF FF FF FF FF FF FF FF "	//dummy
		,
		//side map350 for stage3-2
		"FF FF FF FF FF FF FF FF "	//dummy
		,
		//side map360 for stage3-3
		"FF FF FF FF FF FF FF FF "	//dummy
		,
		//side mapdummy
		"FF FF FF FF FF FF FF FF "	//dummy
		,
		#if MAP_EDITOR
		//side map for editor
		"FF FF FF FF FF FF FF FF "	//dummy
		#endif
	};
		
	//map data
	int map_xmax = 8;	//map raw data x size
	int[] map_ymax;		//map raw data y size

	//side map data
	int smap_xmax = 8;	//side map raw data x size
	int[] smap_ymax;	//side map raw data y size

	//map draw
	const float xoffset = -3.5f;	//map draw offset x
	const float yoffset = -6.0f;	//map draw offset y
	const int map_xsize = 8;	//map draw size x
	const int map_ysize = 12;	//map draw size y
	const int mapNull = 0xff;	//map sprite null data
	const float mpxmax = 0.6f;	//map x scroll max (mapmax 1.15)

	//side map draw
	const float smxoffset = -3.5f;	//side map draw offset x
	const float smyoffset = -6.0f;	//side map draw offset y
	const int smap_xsize = 8;	//side map draw size x
	const int smap_ysize = 12;	//side map draw size y
	const int smapNull = 0xff;	//side map sprite null data
	const float smpxmax = 0.9f;	//side map x scroll max (mapmax 1.15)

	//back star
	const int backstar_num = 65;	//back star num

	//map raw data
	int ysize_rawdat;

	//side map raw data
	int smysize_rawdat;

	//local
	//component cash
	GameObject mainCtr;
	mainController mc;

	//system
	int intervalCnt;	//interval counter

	//(current) scroll speed for getter
	float scrSpd;

	//map data
	int mapdata_index;

	//side map data
	int smapdata_index;

	//map raw
	int[,] map_raw;

	//side map raw
	int[,] smap_raw;

	//map,side map sprite(map parts) array
	Sprite[] mpspr;

	//map parts
	GameObject[] mp = new GameObject[ map_xsize * map_ysize ];
	mapPartsController[] mpctl = new mapPartsController[ map_xsize * map_ysize ];

	//side map parts
	GameObject[] smp = new GameObject[ smap_xsize * smap_ysize ];
	sideMapPartsController[] smpctl = new sideMapPartsController[ smap_xsize * smap_ysize ];

	//map stage
	int mapStage;

	//map control
	int mapNextCnt_x;

	//side map control
	int smapNextCnt_x;

	//target scroll speed
	float tScrSpd;

	//current scroll speed
	float cScrSpd;

	//scroll x pos
	float mpxpos;

	//scroll x move for getter
	float mpxmov;

	//back star
	GameObject[] bs = new GameObject[backstar_num];


	// Use this for initialization
	void Start () {
		//cash

		//system init
		intervalCnt = 0;

		//cash
		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//map stage
		mapStage = msdummy;

		//map next x cnt init
		mapNextCnt_x = 0;

		//side map next x cnt init
		smapNextCnt_x = 0;

		//target scroll speed
		tScrSpd = 0.0f;

		//current scroll speed
		cScrSpd = 0.0f;

		//public scroll speed
		scrSpd = cScrSpd;

		//scroll x pos
		mpxpos=0;

		//scroll x move
		mpxmov=0;

		//map raw data y max
		map_ymax = new int[]{
			(mapdata[ms50].Length/3/map_xmax),	//map50
			(mapdata[ms100].Length/3/map_xmax),	//map100
			(mapdata[ms200].Length/3/map_xmax),	//map200
			(mapdata[ms300].Length/3/map_xmax),	//map300
			(mapdata[ms350].Length/3/map_xmax),	//map350
			(mapdata[ms360].Length/3/map_xmax),	//map360
			(mapdata[msdummy].Length/3/map_xmax),	//map dummy
			#if MAP_EDITOR
			(mapdata[msEdit].Length/3/map_xmax),	//map for map editor
			#endif
		};

		//side map raw data y max
		smap_ymax = new int[]{
			(smapdata[sms50].Length/3/smap_xmax),	//side map50
			(smapdata[sms100].Length/3/smap_xmax),	//side map100 (none)
			(smapdata[sms200].Length/3/smap_xmax),	//side map200 (none)
			(smapdata[sms300].Length/3/smap_xmax),	//side map300 (none)
			(smapdata[sms350].Length/3/smap_xmax),	//side map350 (none)
			(smapdata[sms360].Length/3/smap_xmax),	//side map360 (none)
			(smapdata[smsdummy].Length/3/smap_xmax),	//side map dummy
			#if MAP_EDITOR
			(smapdata[smsEdit].Length/3/smap_xmax),	//side map for map editor (none)
			#endif
		};

		//map raw data init
		ysize_rawdat = map_ymax[ms50];	//最大サイズのmap分確保 (raw dat配列サイズ固定)
		if (ysize_rawdat < map_ymax [ms100]) {
			ysize_rawdat = map_ymax [ms100];
		}
		if (ysize_rawdat < map_ymax [ms200]) {
			ysize_rawdat = map_ymax [ms200];
		}
		if (ysize_rawdat < map_ymax [ms300]) {
			ysize_rawdat = map_ymax [ms300];
		}
		if (ysize_rawdat < map_ymax [ms350]) {
			ysize_rawdat = map_ymax [ms350];
		}
		if (ysize_rawdat < map_ymax [ms360]) {
			ysize_rawdat = map_ymax [ms360];
		}
		#if MAP_EDITOR
		if (ysize_rawdat < map_ymax [msEdit]) {
			ysize_rawdat = map_ymax [msEdit];
		}
		#endif
		map_raw = new int[map_xsize, ysize_rawdat ];	//map raw data	//最大mapサイズ分確保
		for (int y = 0 ; y < ysize_rawdat ; y++) {
			for (int x = 0; x < map_xsize; x++) {
				map_raw [x, y] = mapNull;	//null sprite
			}
		}

		//side map raw data init
		smysize_rawdat = smap_ymax[sms50];	//最大サイズ(side map50)で確保 (raw dat配列サイズ固定)
		smap_raw = new int[smap_xsize, smysize_rawdat ];	//side map raw data	//最大mapサイズ分確保
		for (int y = 0 ; y < smysize_rawdat ; y++) {
			for (int x = 0; x < smap_xsize; x++) {
				smap_raw [x, y] = smapNull;	//side map null sprite
			}
		}

		//map parts sprite load
		mpspr = Resources.LoadAll<Sprite>("sprites/maps");

		//map data read index init
		mapdata_index = 0;

		//side map data read index init
		smapdata_index = 0;

		//map parts generate and init
		for (int yy = 0; yy < map_ysize ; yy++) {
			for (int xx = 0; xx < map_xsize ; xx++) {
				mp[(yy*map_xsize)+xx] = Instantiate (mapPartsPrefab) as GameObject;
				mpctl [(yy * map_xsize) + xx] = mp [(yy * map_xsize) + xx].GetComponent<mapPartsController> ();
				#if !MAP_EDITOR
				mpctl [(yy * map_xsize) + xx].setInitStatus ((xoffset + (float)xx), (yoffset + (float)(map_ysize - yy - 1)), xx, null);
				#else
				//map reduce for map editor
				mp[(yy*map_xsize)+xx].transform.localScale = new Vector3( mescale, mescale, 1.0f );
				mpctl [(yy * map_xsize) + xx].setInitStatus (((xoffset*mescale) + ((float)xx*mescale)), ((yoffset*mescale) + (float)(((float)map_ysize*mescale) - (((float)yy*mescale)) - (1*mescale))), xx, null);
				#endif
			}
		}

		//side map parts init
		for (int yy = 0; yy < smap_ysize ; yy++) {
			for (int xx = 0; xx < smap_xsize ; xx++) {
				smp [(yy * smap_xsize) + xx] = null;
			}
		}

		//back star init
		for (int i = 0; i < backstar_num; i++) {
			bs [i] = null;
		}

		#if MAP_EDITOR
		this.mapEditorStart();
		#endif
	}
	
	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {
		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}

		//////always process

		//nop

		//////interval process
		intervalCnt++;
		if (intervalCnt >= 1) {
			intervalCnt = 0;

			//map x scroll
			mpxmov = mc.getMapxScroll();
			if ( mpxpos + mpxmov >= mpxmax ) {
				mpxmov = mpxmax - mpxpos;
			} else if( mpxpos + mpxmov <= (mpxmax * -1) ){
				mpxmov = (mpxmax * -1) - mpxpos;
			}
			mpxpos = mpxpos + mpxmov;

			//scroll speed change
			if (tScrSpd > cScrSpd) {
				//current to target scroll speed
				float backScr = cScrSpd;
				cScrSpd = cScrSpd + 0.0075f;	//acceleration
				if (tScrSpd <= cScrSpd) {
					cScrSpd = tScrSpd;
				}
				//reverse
				if ((backScr < 0) && (cScrSpd >= 0)) {
					mapdata_index = mapdata_index - map_ysize - 1;
					if ( mapdata_index < 0 ) {
						mapdata_index = map_ymax [mapStage] + mapdata_index;
					}
					if ((mapStage == sms50)) {
						smapdata_index = smapdata_index - smap_ysize - 1;
						if (smapdata_index < 0) {
							smapdata_index = smap_ymax [mapStage] + smapdata_index;
						}
					}
				}
			} else if (tScrSpd < cScrSpd) {
				//current to target scroll speed
				float backScr = cScrSpd;
				cScrSpd = cScrSpd - 0.0055f;	//brake
				if (tScrSpd >= cScrSpd) {
					cScrSpd = tScrSpd;
				}
				//reverse
				if ((backScr >= 0) && (cScrSpd < 0)) {
					mapdata_index = mapdata_index + map_ysize + 1;
					if ( mapdata_index > (map_ymax [mapStage] - 1)) {
						mapdata_index = 0 + (mapdata_index-map_ymax[mapStage]);
					}
					if ((mapStage == sms50)) {
						smapdata_index = smapdata_index + smap_ysize + 1;
						if (smapdata_index > (smap_ymax [mapStage] - 1)) {
							smapdata_index = 0 + (smapdata_index - smap_ymax [mapStage]);
						}
					}
				}
			}
			//public scroll speed set
			scrSpd = cScrSpd;

			#if MAP_EDITOR
			this.mapEditorUpdate();
			#endif

		}		
	}

	//private

	//generate side map
	private void generateSideMap(){
		//side map parts generate and init
		for (int yy = 0; yy < smap_ysize ; yy++) {
			for (int xx = 0; xx < smap_xsize ; xx++) {
				if (smp [(yy * smap_xsize) + xx] == null) {
					//generate side map parts
					smp [(yy * smap_xsize) + xx] = Instantiate (sideMapPartsPrefab) as GameObject;
					smpctl [(yy * smap_xsize) + xx] = smp [(yy * smap_xsize) + xx].GetComponent<sideMapPartsController> ();
					smpctl [(yy * map_xsize) + xx].setInitStatus ((smxoffset + (float)xx), (smyoffset + (float)(smap_ysize - yy - 1)), xx, null);
					//inc obj
					mc.incObj ();
				}
			}
		}
	}

	//delete side map
	private void deleteSideMap(){
		//side map parts delete
		for (int yy = 0; yy < smap_ysize ; yy++) {
			for (int xx = 0; xx < smap_xsize ; xx++) {
				if (smp [(yy * smap_xsize) + xx] != null) {
					//delete side map parts
					GameObject.Destroy (smp [(yy * smap_xsize) + xx]);
					smp [(yy * smap_xsize) + xx] = null;
					//dec obj
					mc.decObj ();
					//side map controller null
					smpctl [(yy * smap_xsize) + xx] = null;
				}
			}
		}
	}

	//generate back star
	private void generateBackStar(){
		for (int i = 0; i < backstar_num; i++) {
			if (bs [i] == null) {
				//generate back star
				bs[i] = Instantiate (backStarPrefab) as GameObject;
//				bs[i].GetComponent<backStarController> ().setInitStatus ();
				//inc obj
				mc.incObj();
			}
		}
	}

	//delete back star
	private void deleteBackStar(){
		for (int i = 0; i < backstar_num; i++) {
			if (bs [i] != null) {
				//delete back star
				GameObject.Destroy (bs [i]);
				bs [i] = null;
				//dec obj
				mc.decObj ();
			}
		}
	}


	////public

	//map new line set
	public Sprite getMapNextData( int mapParts_x ){
		Sprite sp;
		int spno = map_raw [mapParts_x, mapdata_index];
		if (spno == mapNull) {
			sp = null;
		}else{
			sp = mpspr [map_raw [mapParts_x, mapdata_index]];	//next sprite
		}

		//1 line set?
		mapNextCnt_x++;
		if (mapNextCnt_x >= map_xsize) {
			//next line setting
			mapNextCnt_x = 0;
			if (cScrSpd >= 0) {
				mapdata_index--;
				if (mapdata_index < 0) {
					mapdata_index = map_ymax [mapStage] - 1;
				}
			} else {
				mapdata_index++;
				if ( mapdata_index > (map_ymax [mapStage] - 1)) {
					mapdata_index = 0;
				}
			}
		}
		return sp;	//next sprite
	}

	//sidemap new line set
	public Sprite getSideMapNextData( int smapParts_x ){
		Sprite sp;
		int spno = smap_raw [smapParts_x, smapdata_index];
		if (spno == smapNull) {
			sp = null;
		}else{
			sp = mpspr [smap_raw [smapParts_x, smapdata_index]];	//next sprite
		}

		//1 line set?
		smapNextCnt_x++;
		if (smapNextCnt_x >= smap_xsize) {
			//next line setting
			smapNextCnt_x = 0;
			smapdata_index--;
			if (cScrSpd >= 0) {
				if (smapdata_index < 0) {
					smapdata_index = smap_ymax [mapStage] - 1;
				}
			} else {
				smapdata_index++;
				if ( smapdata_index > (smap_ymax [mapStage] - 1)) {
					smapdata_index = 0;
				}
			}
		}
		return sp;	//next sprite
	}

	//set map stage
	public void setMapStage( int ms ){
		#if !MAP_EDITOR
		//same stage?
		if (this.mapStage == ms) {
			return;
		}
		#endif

		//map stage set
		mapStage = ms;

		//camera back color
		if ( (mapStage == ms50) || (mapStage == ms100) || (mapStage == ms200) || (mapStage == ms300) || (mapStage == ms360) || (mapStage == msdummy) ) {
			Camera.main.backgroundColor = new Color (11.0f / 255.0f, 18.0f / 255.0f, 35.0f / 255.0f, 255.0f / 255.0f);
		}
		#if MAP_EDITOR
		if ( mapStage == msEdit ){
			Camera.main.backgroundColor = new Color (11.0f / 255.0f, 18.0f / 255.0f, 35.0f / 255.0f, 255.0f / 255.0f);
		}
		#endif
		if ( mapStage == ms350 ){
			Camera.main.backgroundColor = new Color (48.0f / 255.0f, 11.0f / 255.0f, 18.0f / 255.0f, 255.0f / 255.0f);
		}

		//set map raw data
		int ccnt=0;	//map data index
		for (int yy = 0; yy < map_ymax[mapStage]; yy++) {
			for (int xx = 0; xx < map_xmax; xx++) {
				int val = int.Parse( mapdata[mapStage].Substring (ccnt, 2), System.Globalization.NumberStyles.HexNumber);	//hex string -> int
				map_raw[xx,yy] = val;
				ccnt = ccnt + 3;
			}
		}

		//map parts init
		int md;
		Sprite sp=null;
		for (int yy = 0; yy < map_ysize ; yy++) {
			for (int xx = 0; xx < map_xsize ; xx++) {
				md = map_raw [xx, map_ymax [mapStage] - map_ysize + yy];
				if (md == mapNull) {
					sp = null;
				} else {
					sp = mpspr [ md ];
				}
				mpctl [(yy * map_xsize) + xx].initPos();
				mpctl [(yy * map_xsize) + xx].setSprite (sp);
			}
		}

		//map raw data read index init
		mapdata_index = map_ymax[mapStage] -1 - map_ysize;
		if (mapdata_index < 0) {
			mapdata_index = map_ymax[mapStage] - 1;
		}
		mapNextCnt_x = 0;

		//set side map
		if ((mapStage == sms50)) {
			//generate side map parts
			this.generateSideMap();

			//set side map raw data
			ccnt = 0;	//map data index
			for (int yy = 0; yy < smap_ymax [mapStage]; yy++) {
				for (int xx = 0; xx < smap_xmax; xx++) {
					int val = int.Parse (smapdata [mapStage].Substring (ccnt, 2), System.Globalization.NumberStyles.HexNumber);	//hex string -> int
					smap_raw [xx, yy] = val;
					ccnt = ccnt + 3;
				}
			}

			//side map parts init
			sp = null;
			for (int yy = 0; yy < smap_ysize; yy++) {
				for (int xx = 0; xx < smap_xsize; xx++) {
					md = smap_raw [xx, smap_ymax [mapStage] - smap_ysize + yy];
					if (md == smapNull) {
						sp = null;
					} else {
						sp = mpspr [md];
					}
					smpctl [(yy * smap_xsize) + xx].initPos ();
					smpctl [(yy * smap_xsize) + xx].setSprite (sp);
				}
			}

			//side map raw data read index init
			smapdata_index = smap_ymax [mapStage] - 1 - smap_ysize;
			if (smapdata_index < 0) {
				smapdata_index = smap_ymax [mapStage] - 1;
			}
			smapNextCnt_x = 0;
		} else {
			//delete side map parts
			this.deleteSideMap();
		}

		//back star set or delete
		if ((mapStage == ms50) || (mapStage == ms200) || (mapStage == ms300) || (mapStage == ms350) || (mapStage == ms360)) {
			this.generateBackStar ();
		} else {
			this.deleteBackStar ();
		}
	}

	//set target scroll speed
	public void setTargetScrollSpeeed( float tSpd ){
		//target scroll speed
		tScrSpd = tSpd;
	}

	//set scroll speed
	public void setScrollSpeeed( float tSpd ){
		float backScr = cScrSpd;
		//scroll speed
		tScrSpd = tSpd;
		cScrSpd = tSpd;
		scrSpd = tSpd;
		//reverse
		if ((backScr < 0) && (cScrSpd >= 0)) {
			mapdata_index = mapdata_index - map_ysize - 1;
			if ( mapdata_index < 0 ) {
				mapdata_index = map_ymax [mapStage] + mapdata_index;
			}
			if ((mapStage == sms50)) {
				smapdata_index = smapdata_index - smap_ysize - 1;
				if (smapdata_index < 0) {
					smapdata_index = smap_ymax [mapStage] + smapdata_index;
				}
			}
		}
		if ((backScr >= 0) && (cScrSpd < 0)) {
			mapdata_index = mapdata_index + map_ysize + 1;
			if ( mapdata_index > (map_ymax [mapStage] - 1)) {
				mapdata_index = 0 + (mapdata_index-map_ymax[mapStage]);
			}
			if ((mapStage == sms50)) {
				smapdata_index = smapdata_index + smap_ysize + 1;
				if (smapdata_index > (smap_ymax [mapStage] - 1)) {
					smapdata_index = 0 + (smapdata_index - smap_ymax [mapStage]);
				}
			}
		}
	}

	//(current) scroll speed getter
	public float getScrSpd(){
		return this.scrSpd;
	}

	//get map x pos
	public float getMapxPos(){
		return this.mpxpos;
	}

	//get map x mov
	public float getMapxMov(){
		return this.mpxmov;
	}

	//set map x reset
	public void setMapxReset(){
		//map x reset
		this.mpxpos = 0.0f;
		//map parts reset
		for (int yy = 0; yy < map_ysize ; yy++) {
			for (int xx = 0; xx < map_xsize ; xx++) {
				mpctl[(yy*map_xsize)+xx].resetMapx( (xoffset + (float)xx) );
			}
		}	
		//side map parts reset
		if ((mapStage == sms50)) {
			for (int yy = 0; yy < smap_ysize; yy++) {
				for (int xx = 0; xx < smap_xsize; xx++) {
					if (smpctl [(yy * smap_xsize) + xx] != null) {
						smpctl [(yy * smap_xsize) + xx].resetMapx ((smxoffset + (float)xx));
					}
				}
			}	
		}
	}

}
