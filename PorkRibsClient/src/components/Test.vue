<template>
    <div>
        <h1>Test</h1>      
        <span v-if="numbers.error" class="text-danger">ERROR: {{numbers.error}}</span>
        <ul v-if="numbers">
            <li v-for="number in numbers" :key="number">{{number}}</li>
        </ul>
    </div>
</template>

<script>

import axios from 'axios';

export default{
    data:()=>({
        numbers:[]
    }),

    created(){
        axios.get('https://localhost:4445/api/test')
        .then(response=>{
            this.numbers = response.data
        })
        .catch(e=>{
        })
    }
}
</script>