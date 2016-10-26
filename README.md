# Kata: Tasteful composition of side-effects in the real world

### What does this kata teach?

* Tasteful composition of side-effects in the real world
* Foldable and Traversable concepts
* Real world example combining Functor, Applicative, Monad, Foldable, Traversable

### How to use this kata?

0. Speed read the references below (OK to understand very little to start with, emphasis is on building finger memory)
1. Clone repo
2. Checkout Step 0
3. Refer changes in each step, make the same changes on your own, commit
4. Repeat steps 2 to 4 above, until you have built finger memory for the changes

NOTE: Fully understanding the theory is not required to start with. Emphasis is on building finger memory.

### Steps

* Step 0. README & basic plumbing
* Step 1. Domain model and utility functions
* Step 2. Implement Functor, Applicative and Monad interfaces for Async type
* Step 3. Implement Traversable interfaces for List type (Applicative flavor)
* Step 4. End to end functionality wired up (Applicative flavor)
* Step 5. Implement Traversable interfaces for List type (Monadic flavor)
* Step 6. End to end functionality wired up (Monadic flavor)
* Step 7. Treating two worlds as one fixes the issue with the Monadic flavor

### The Kata Scenario:

Given a list of websites, create an action that finds the site with the largest home page.

### References:

* [Using map, apply, bind and sequence in practice](https://fsharpforfunandprofit.com/posts/elevated-world-5/)
* [Sequence and Traverse](https://sidburn.github.io/blog/2016/04/14/sequence-and-traverse)
