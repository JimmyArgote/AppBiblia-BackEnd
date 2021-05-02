<template>
    <div id="app">
        <Navbar/>
        <router-view :key="$route.fullPath"></router-view>
    </div>
</template>

<script>
import Navbar from '../modules/Navbar.vue'

export default {
    name: 'AppHome',
    components: {Navbar},

    
}
</script>