﻿using System;
using EndlessAdventure.Common.Battle;
using Xunit;

namespace EndlessAdventure.Common.Tests {
	public class Test {
		[Fact]
		public void TestTrue () {
			Assert.True(true);
		}

		[Fact]
		public void TestFalse() {
			Assert.False(false);
		}
		
	}
}