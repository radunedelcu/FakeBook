<template>
    <div class="card-body">
        <form>
            <div class="post mb-4">
                <div class="h-100">
                    <div class="bg-white p-4 h-100 position-relative">
                        <div style="mask height:80%; overflow-y:scroll; flex-flow:column">
                            <div class="expandable-image d-flex flex-start align-items-center" style="flex: 0 1 auto">
                                <div class="image-container">
                                    <ExpandableImage class="profileImage" v-bind:src="'https://localhost:7026/' + message.userProfilePicture" alt="avatar" />
                                </div>
                                <div class="info">
                                    <h6 class="fw-bold text-primary mb-1"> {{message.name}}</h6>
                                    <p class="text-muted small mb-0">
                                        {{message.createdDate}}
                                    </p>
                                </div>
                            </div>

                            <p class="mt-3 mb-4 pb-2">
                                {{message.message}}
                            </p>
                            <div class="image-container">
                                <ExpandableImage v-bind:src="'https://localhost:7026/' + message.imagePath" />
                            </div>
                            <div class="small d-flex justify-content-start">
                                <a href="#!" class="align-items-center me-3">
                                    <i class="far fa-thumbs-up me-2"></i>
                                    <p class="mb-0">Like</p>
                                </a>

                            </div>
                            <div>
                                <span></span>
                            </div>
                        </div>
                        <div class=" py-3 border-0" style="flex: 1 1 auto;">
                            <div class="d-flex flex-start w-100">

                                <div class="form-outline w-100">
                                    <textarea class="form-control" id="textAreaExample" rows="4"
                                        style="background: #fff;"></textarea>
                                    <label class="form-label" for="textAreaExample">Message</label>
                                </div>
                            </div>
                            <div class="d-flex justify-content-end">
                                <button type="button" class="btn btn-primary btn-sm">Post comment</button>
                                <button type="button" class="btn btn-outline-primary btn-sm">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </form>
    </div>

</template>

<script>
    import ExpandableImage from "../components/ExpandableImage.vue"
export default {
    props: {
        message: []
    },
    created() {
        console.log(this.message);
    },
    components: { ExpandableImage },
}
</script>

<style>

</style>