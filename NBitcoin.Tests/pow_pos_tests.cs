﻿using System.IO;
using System.Linq;
using NBitcoin.BitcoinCore;
using Xunit;

namespace NBitcoin.Tests
{
	public class pow_pos_tests
	{

		[Fact]
		[Trait("UnitTest", "UnitTest")]
		public static void CanCalculatePowPosCorrectly()
		{
			var chain = new ConcurrentChain(File.ReadAllBytes(TestDataLocations.BlockHeadersLocation));

			foreach (var block in chain.EnumerateAfter(chain.Genesis))
			{
				Assert.True(block.CheckPowPosAndTarget(Network.Main));
			}
		}

		[Fact]
		[Trait("UnitTest", "UnitTest")]
		public static void CheckBlockSignatureSerialization()
		{
			// validate a selection of block signatures

			var store = new BlockStore(TestDataLocations.BlockFolderLocation, Network.Main);

			foreach (var block in store.EnumerateFolder().Take(30000))
			{
				var hash = block.Item.GetHash();
				if(hash == uint256.Parse("d901e04f253bbcb10d842e4c3339c277e0862a02c6dab21f0288b9c9c839856c")) Assert.Equal(block.Item.BlockSignatur.ToString(), "3045022100a68a6b1e5310fe2e7eb565d7dc035952c5d2b79495de721eef602bf0258eeeb10220034a914dbf02e72c3d99c86cb043757be96853bed9f1c9508969796402ff44c5");
				if (hash == uint256.Parse("49be6e0ba43b614f361e764b984223fbb1fdee312efc98354b20ccba8219854e")) Assert.Equal(block.Item.BlockSignatur.ToString(), "304402205278aa1fdac9a492527fb94a071b5cd839b3eca22aec9e9ee89d203790f286570220690f9a696fa482b7ebb421fee0aebbcbce82ec0b884f88304a5090326b349ac7");
				if (hash == uint256.Parse("541d330e312afd1545b01ef60cba5f94d35e22fb91559c12978c7b6a4e67b854")) Assert.Equal(block.Item.BlockSignatur.ToString(), "30440220471f1da87351728e5824c1b761b27e6b211a81e4da2847484c329723cce1a1f0022007d9e9aba5527601f7c22473f7295529bcf7b351a8923ea3d159a1a30e14ffe4");
				if (hash == uint256.Parse("c477a0c0cfb9231f6cf2480a1b7499ea62150118b921dedcf70b56378de6b877")) Assert.Equal(block.Item.BlockSignatur.ToString(), "30440220116c697184df53abf9e54d9d198a3fec709996f0aea3ccaa57a800272595655f0220233f038a47fc04fef5d4635b241afa43ee9cb47bdf416df2602171219404df06");
				if (hash == uint256.Parse("895a547a9fdef74835d9a3c47780fafea70c6989a16aa05c0a287980c69a8529")) Assert.Equal(block.Item.BlockSignatur.ToString(), "304402200f27e4d8c0ec8baff0d2561ca8396794434503bfdcb6497382191539a24cb436022021d4ddff020c1e3386d55010327abfc26258af7a44a56a49e6662c9ce81d8f96");
				if (hash == uint256.Parse("e29a64e77957bafb984368df2635e1f9ff5ac93f985fd3cbd8cc812f013a7458")) Assert.Equal(block.Item.BlockSignatur.ToString(), "30440220099f604dbabc48bbd19aeb59e76b923e8cdb6142d3413211ec2cc0aa7889e8e60220690316b26d4f6797165a52dd4b4e9df17964edc12b919c5af9430787f6766d40");
				
			}
		}

		[Fact]
		[Trait("UnitTest", "UnitTest")]
		public static void CheckBlockSignature()
		{
			// validate all block signatures

			var store = new BlockStore(TestDataLocations.BlockFolderLocation, Network.Main);

			foreach (var block in store.EnumerateFolder().Take(30000))
			{
				Assert.True(BlockValidator.CheckBlockSignature(block.Item));
			}
		}
	}
}
