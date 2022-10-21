<template>

    <div>
        <!-- Column -->
        <div class="cover">
            <div class="coverImg">
                <ExpandableImage id="image1"  class="card-img-top" src="https://i.imgur.com/K7A78We.jpg"
                alt="Card image cap"/>
            </div>
        </div>
        
        <div class="card-body little-profile text-center">
            <div class="pro-img">
                <img src="https://i.imgur.com/8RKXAIV.jpg" alt="user">
            </div>
            <h3 class="m-b-0">{{currentUser.name}}</h3>
            <p>{{currentUser.quote}}</p> <a href="javascript:void(0)"
                class="m-t-10 waves-effect waves-dark btn btn-primary btn-md btn-rounded" data-abc="true">Add Friend</a>
            <div class="row text-center m-t-20">
                <div class="col-lg-4 col-md-4 m-t-20">
                    <h3 class="m-b-0 font-light">10434</h3><small>Articles</small>
                </div>
                <div class="col-lg-4 col-md-4 m-t-20">
                    <h3 class="m-b-0 font-light">434K</h3><small>Followers</small>
                </div>
                <div class="col-lg-4 col-md-4 m-t-20">
                    <h3 class="m-b-0 font-light">5454</h3><small>Following</small>
                </div>
            </div>
        </div>
    </div>

</template>
  
<script>
import ExpandableImage from '../../components/ExpandableImage.vue'
export default {
    name: 'ProfilePage',
    computed: {
        currentUser() {
            return this.$store.state.auth.user;
        }
    },
    // mounted() {
    //   if (!this.currentUser) {
    //     this.$router.push('/login');
    //   }
    // }
    components: { ExpandableImage }
};
</script>

<style scoped>
body {
    background-color: #000000
}

.card-img-top {
    height: 300px !important;
 
}

.card-body {
    -ms-flex: 1 1 auto;
    flex: 1 1 auto;
    padding: 1.25rem
}

.pro-img {
    margin-top: -80px;
    margin-bottom: 20px
}

.coverImg {
    width: 100%;
}

.little-profile .pro-img img {
    width: 128px;
    height: 128px;
    -webkit-box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
    box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
    border-radius: 100%
}

html body .m-b-0 {
    margin-bottom: 0px
}

h3 {
    line-height: 30px;
    font-size: 21px
}

.btn-rounded.btn-md {
    padding: 12px 35px;
    font-size: 16px
}

html body .m-t-10 {
    margin-top: 10px
}

.cover {
    width: 100%;
}

.btn-primary,
.btn-primary.disabled {
    background: blue;
    border: 1px solid #7460ee;
    -webkit-box-shadow: 0 2px 2px 0 rgba(116, 96, 238, 0.14), 0 3px 1px -2px rgba(116, 96, 238, 0.2), 0 1px 5px 0 rgba(116, 96, 238, 0.12);
    box-shadow: 0 2px 2px 0 rgba(116, 96, 238, 0.14), 0 3px 1px -2px rgba(116, 96, 238, 0.2), 0 1px 5px 0 rgba(116, 96, 238, 0.12);
    -webkit-transition: 0.2s ease-in;
    -o-transition: 0.2s ease-in;
    transition: 0.2s ease-in
}

.btn-rounded {
    border-radius: 60px;
    padding: 7px 18px
}

.m-t-20 {
    margin-top: 20px
}

.text-center {
    text-align: center !important
}

#image1 {
    width: 100%;
}

h1,
h2,
h3,
h4,
h5,
h6 {
    color: #455a64;
    font-family: "Poppins", sans-serif;
    font-weight: 400
}

p {
    margin-top: 0;
    margin-bottom: 1rem
}
</style>