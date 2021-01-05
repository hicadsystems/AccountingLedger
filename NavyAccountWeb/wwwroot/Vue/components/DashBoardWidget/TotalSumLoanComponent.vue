<template>
    <!-- WRAPPER -->
    <div>
        <h2 class="m-t-0"> {{ this.surplusValue2 }}</h2>
    </div>

    <!-- END WRAPPER -->
</template>

<script>
    export default {
        props: ['fundtypeid', 'statusid'],
        data() {
            return {
                surplusValue2: 0,
            }
        },
        mounted() {
            axios
                .get(`/api/DashBoardWidget/getLoanSum/${this.fundtypeid}`)
                .then(response => (this.surplusValue2 = response.data.data))

        }
    };
</script>