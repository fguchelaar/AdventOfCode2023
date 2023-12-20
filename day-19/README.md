# Day 19: Aplenty

Pretty cool problem. For part 1 I just 'naively' perform each worflow and rules
on the parts. Tally up the ratings and ⭐️.

Part 2 is a lot more interesting. Problem space is way to big to brute force, so
I came up with a way to 'split' the ranges 1-4000 based on the rules. For each
rule I find the min and max values that are allowed. Put all accepted (A) ranges
in a list and sum them up in the end.

It's quite verbose, but I'm fine with that.

⭐️⭐️ https://adventofcode.com/2023/day/19
