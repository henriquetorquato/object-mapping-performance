# Object Mapping Performance Comparison

## Current run results:

Mapping performance for `SimpleStringObject`:

|Iterations     |10000          |30000          |50000          |70000          |90000          |
|---------------|---------------|---------------|---------------|---------------|---------------|
|Attribution    |5ms            |21ms           |33ms           |57ms           |64ms           |
|Reflection     |19ms           |60ms           |88ms           |125ms          |145ms          |
|Serialization  |384ms          |222ms          |222ms          |265ms          |344ms          |
|ExpressionTree |8ms            |9ms            |15ms           |22ms           |30ms           |
|AutoMapper     |40ms           |9ms            |16ms           |22ms           |29ms           |

Mapping performance for `ValueConversionObject`:

|Iterations     |10000          |30000          |50000          |70000          |90000          |
|---------------|---------------|---------------|---------------|---------------|---------------|
|Attribution    |4ms            |13ms           |22ms           |31ms           |39ms           |
|Reflection     |15ms           |46ms           |84ms           |110ms          |143ms          |
|Serialization  |54ms           |132ms          |222ms          |298ms          |373ms          |
|ExpressionTree |7ms            |11ms           |19ms           |27ms           |34ms           |
|AutoMapper     |7ms            |9ms            |18ms           |24ms           |29ms           |