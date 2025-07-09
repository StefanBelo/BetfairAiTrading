# My attempt to use Gemini CLI to write a simple F# trading strategy

I've been experimenting with the Gemini CLI to see if it could help automate some of my F# coding for a Betfair trading bot. I wanted it to create a simple strategy script.

The first attempt was a complete failure. The code it generated was full of bugs and wouldn't even compile.

So, I decided to give it a helping hand. I wrote the script myself, got it working perfectly, and then gave it to the agent. I asked it to analyze my correct implementation and revise the initial prompt, hoping it would learn the required structure and coding standards.

After a few back-and-forths to get the prompt right, I asked it to generate the script again. At first glance, it looked like it had just copied my code, which was a start. But then I saw it had added its own `GetInfo` function directly into the `IBotTrigger` interface implementation block. This is a syntax error in F# and shows a fundamental lack of understanding of the language's structure.

It feels like I'm hitting a wall here. Even with a correct example to work from, the AI couldn't produce valid F# code for a relatively simple task. It seems like Gemini CLI, at least for F#, isn't quite ready for prime time. It's a cool concept, but the execution is just not there yet.

Has anyone else had similar experiences with AI code generation for F# or other less-common languages?
